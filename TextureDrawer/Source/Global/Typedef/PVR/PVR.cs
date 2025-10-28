using System;
using System.IO;
using System.Runtime.InteropServices;
using SkiaSharp;

// Supports PowerVR Texture Compression (PVRTC)

public static unsafe class PVR
{
// Precomputed tables

private static readonly byte[] RepVals2BPP = new byte[16];
private static readonly byte[] RepVals4BPP = new byte[16];

private static readonly int[,] PxOffset2BPP = new int[8, 4];
private static readonly int[,] PxOffset4BPP = new int[4, 4];

// Bilinear factors

private static readonly byte[][] BILINEAR_FACTORS = 
[
    [ 4, 4, 4, 4 ],
	[ 2, 6, 2, 6 ],
    [ 8, 0, 8, 0 ],
    [ 6, 2, 6, 2 ],
	[ 2, 2, 6, 6 ],
	[ 1, 3, 3, 9 ],
    [ 4, 0, 12, 0 ],
	[ 3, 1, 9, 3 ],
	[ 8, 8, 0, 0 ],
	[ 4, 12, 0, 0 ],
	[ 16, 0, 0, 0 ],
	[ 12, 4, 0, 0 ],
	[ 6, 6, 2, 2 ],
	[ 3, 9, 1, 3 ],
	[ 12, 0, 4, 0 ],
	[ 9, 3, 3, 1 ], 

];

// Word Height

private const int WORD_HEIGHT = 4;

// Init Tables

static PVR()
{
// RepVals

for(byte i = 0; i < 16; i++)
{
RepVals2BPP[i] = (byte)( (i & 1) | ( (i & 2) << 1) | ( (i & 4) >> 1) | ( (i & 8) >> 2) );
RepVals4BPP[i] = i;
}

// Pixel Offsets (2BPP)

for(int y = 0; y < 4; y++)
{

for(int x = 0; x < 8; x++)
PxOffset2BPP[x, y] = 2 * (x & 3) + 8 * (y & 3);

}

// Pixel Offsets (4BPP)

for(int y = 0; y < 4; y++)
{

for(int x = 0; x < 4; x++)
PxOffset4BPP[x, y] = 4 * x + 16 * y;
 
}

}

// Adjust Image Dimensions

private static bool AdjustSize(ref int width, ref int height, bool is2BPP)
{
int minWidth = is2BPP ? 16 : 8;
int minHeight = 8;

width = Math.Max(width, minWidth);
height = Math.Max(height, minHeight);

return TextureHelper.AdjustSize(ref width, ref height);
}

#region ==========  ENCODER  ==========

// Calculate BoundingBox

private static void CalculateBoundingBox(TextureColor* pixels, int width, int blockX, int blockY,
										 int blockWidth, out TextureColor16 min, out TextureColor16 max)
{
byte minR = 255, minG = minR, minB = minG, minA = minG;
byte maxR = 0, maxG = maxR, maxB = maxG, maxA = maxB;

int start = blockY * WORD_HEIGHT * width + (blockX * blockWidth);

for(int row = 0; row < WORD_HEIGHT; row++)
{

for(int col = 0; col < blockWidth; col++)
{
var px = pixels[start + row * width + col];

minR = Math.Min(minR, px.Red);
minG = Math.Min(minG, px.Green);
minB = Math.Min(minB, px.Blue);
minA = Math.Min(minA, px.Alpha);

maxR = Math.Max(maxR, px.Red);
maxG = Math.Max(maxG, px.Green);
maxB = Math.Max(maxB, px.Blue);
maxA = Math.Max(maxA, px.Alpha);
}

}

min = new(minR, minG, minB, minA);
max = new(maxR, maxG, maxB, maxA);
}



// Init Packets

private static void InitPackets(Span<PVRPacket> packets, TextureColor* pixels, int width, int blocksX,
                                int blocksY, int blockWidth, bool useAlpha)
{

for(int row = 0; row < blocksY; row++)
{

for(int col = 0; col < blocksX; col++)
{
CalculateBoundingBox(pixels, width, col, row, blockWidth, out var min, out var max);

PVRPacket packet = new();

if(useAlpha)
{
packet.SetColorA(min, true);
packet.SetColorB(max, true);
}

else
{
packet.SetColorA(min, false);
packet.SetColorB(max, false);
}

int mortonIdx = Morton.GetIndex(col, row);

packets[mortonIdx] = packet;
}

}

}

// Get Factor (Generic)

private static TextureColor16 GetFactor(in TextureColor16 c0, in TextureColor16 c1, in TextureColor16 c2,
                                        in TextureColor16 c3, ReadOnlySpan<byte> factors)
{
return c0 * factors[0] + c1 * factors[1] + c2 * factors[2] + c3 * factors[3];
}

// Get Factor A

private static TextureColor16 GetFactorA(in PVRPacket p0, in PVRPacket p1, in PVRPacket p2, in PVRPacket p3,
                                         ReadOnlySpan<byte> factors, bool useAlpha)
{
var c0 = p0.GetColorA(useAlpha);
var c1 = p1.GetColorA(useAlpha);
var c2 = p2.GetColorA(useAlpha);
var c3 = p3.GetColorA(useAlpha);

return GetFactor(c0, c1, c2, c3, factors);
}

// Get Factor B

private static TextureColor16 GetFactorB(in PVRPacket p0, in PVRPacket p1, in PVRPacket p2, in PVRPacket p3,
                                         ReadOnlySpan<byte> factors, bool useAlpha)
{
var c0 = p0.GetColorB(useAlpha);
var c1 = p1.GetColorB(useAlpha);
var c2 = p2.GetColorB(useAlpha);
var c3 = p3.GetColorB(useAlpha);

return GetFactor(c0, c1, c2, c3, factors);
}

// Get Pixel Modulation

private static void GetPxMod(Span<PVRPacket> packets, TextureColor* pixels, int width,
                             int x0, int x1, int y0, int y1, int dataOffset, int px,
							 int py, int factorIndex, bool is2BPP, bool useAlpha,
							 ref uint modulationData)
{
var factors = BILINEAR_FACTORS[factorIndex];

int mortonIdx0 = Morton.GetIndex(x0, y0);
int mortonIdx1 = Morton.GetIndex(x1, y0);
int mortonIdx2 = Morton.GetIndex(x0, y1);
int mortonIdx3 = Morton.GetIndex(x1, y1);

var p0 = packets[mortonIdx0];
var p1 = packets[mortonIdx1];
var p2 = packets[mortonIdx2];
var p3 = packets[mortonIdx3];

var ca = GetFactorA(p0, p1, p2, p3, factors, useAlpha);
var cb = GetFactorB(p0, p1, p2, p3, factors, useAlpha);

var pxColor = pixels[dataOffset + py * width + px];

int pR = pxColor.Red << 4;
int pG = pxColor.Green << 4;
int pB = pxColor.Blue << 4;
int pA = useAlpha ? pxColor.Alpha << 4 : 255;

TextureColor16 p = new(pR, pG, pB, pA);

var d = cb - ca;
var v = p - ca;

int projection = (v % d) << 4;
int lengthSquared = d % d;

uint modValue = 0;

if(projection > 3 * lengthSquared)
modValue++;

if(projection > 8 * lengthSquared)
modValue++;

if(projection > 13 * lengthSquared)
modValue++;

int offset = is2BPP ? PxOffset2BPP[px, py] : PxOffset4BPP[px, py];
uint mask = is2BPP ? 0b11u : 0xFu;

modulationData |= (modValue & (mask) ) << offset;
}

// Calculate Block Modulation

private static uint GetBlockMod(Span<PVRPacket> packets, TextureColor* pixels, int width,
								int blocksX, int blocksY, int blockWidth, int bx, int by,
                                bool is2BPP, bool useAlpha)
{
uint modulationData = 0;
int factorIndex = 0;

int dataOffset = by * WORD_HEIGHT * width + (bx * blockWidth);

for(int py = 0; py < WORD_HEIGHT; py++)
{
int y0 = (by + ( (py < 2) ? -1 : 0) + blocksY) % blocksY;
int y1 = (y0 + 1) % blocksY;

for(int px = 0; px < blockWidth; px++)
{
int x0 = (bx + ( (px < (blockWidth / 2) ) ? -1 : 0) + blocksX) % blocksX;
int x1 = (x0 + 1) % blocksX;

GetPxMod(packets, pixels, width, x0, x1, y0, y1, dataOffset,
         px, py, factorIndex, is2BPP, useAlpha, ref modulationData);

factorIndex++;
}

}

return modulationData;
}

// Set Modulations

private static void SetModulations(Span<PVRPacket> packets, TextureColor* pixels, int width, int blocksX,
                                   int blocksY, int blockWidth, bool is2BPP, bool useAlpha)
{

for(int row = 0; row < blocksY; row++)
{

for(int col = 0; col < blocksX; col++)
{
int mortonIdx = Morton.GetIndex(col, row);
ref var packet = ref packets[mortonIdx];

uint modValue = GetBlockMod(packets, pixels, width, blocksX, blocksY,
                            blockWidth, col, row, is2BPP, useAlpha);

packet.ModulationData = modValue;
}

}

}

// Encode Color

private static NativeMemoryOwner<PVRPacket> EncodeColor(TextureColor* pixels, int width, int height,
                                                        bool is2BPP, bool useAlpha)
{
int blockWidth = is2BPP ? 8 : 4;

int blocksX = width / blockWidth;
int blocksY = height / WORD_HEIGHT;

int totalBlocks = blocksX * blocksY;

NativeMemoryOwner<PVRPacket> pOwner = new(totalBlocks);
var packets = pOwner.AsSpan();

InitPackets(packets, pixels, width, blocksX, blocksY, blockWidth, useAlpha);
SetModulations(packets, pixels, width, blocksX, blocksY, blockWidth, is2BPP, useAlpha);

return pOwner;
}

// Encode PVR (Internal)

private static int Encode(Stream writer, ref SKBitmap image, Endianness endian, bool is2BPP, bool useAlpha)
{
TextureHelper.ResizeImage(ref image, (ref int w, ref int h) => AdjustSize(ref w, ref h, is2BPP) );

int width = image.Width;
int height = image.Height;

var pixels = (TextureColor*)image.GetPixels().ToPointer();

TraceLogger.WriteActionStart("Reading pixels...");

using var pOwner = EncodeColor(pixels, width, height, is2BPP, useAlpha);
var packets = pOwner.AsSpan();

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writing raw data...");

var rawBytes = MemoryMarshal.AsBytes(packets);
writer.WriteBytes(rawBytes, endian);

TraceLogger.WriteActionEnd();

return is2BPP ? width : width * 2;
}

// Encode PVR (2BPP)

public static int Encode2bpp(Stream writer, ref SKBitmap image, Endianness endian = default)
{
return Encode(writer, ref image, endian, true, true);
}

// Encode PVR (4BPP)

public static int Encode4bpp(Stream writer, ref SKBitmap image, bool useAlpha, Endianness endian = default)
{
return Encode(writer, ref image, endian, false, useAlpha);
}

#endregion


#region ==========  DECODER  ==========

// Expand to 8-bits

private static byte ExpandBits(int flags, int bits)
{
return (byte)(flags * 255 / ( (1 << bits) - 1) );
}

// Get ColorA

private static TextureColor GetColorA(uint flags)
{
bool isOpaque = (flags & 0x8000) != 0;

byte r, g, b, a;

if(isOpaque)
{
var redMask = (int)( (flags & 0x7c00) >> 10);
r = ExpandBits(redMask, 5);

var greenMask = (int)( (flags & 0x03e0) >> 5);
g = ExpandBits(greenMask, 5);

var blueMask = (int)(flags & 0x001f);
b = ExpandBits(blueMask, 5);

a = 255;
}

else
{
var redMask = (int)( (flags & 0x0f00) >> 8);
r = ExpandBits(redMask, 4);

var greenMask = (int)( (flags & 0x00f0) >> 4);
g = ExpandBits(greenMask, 4);

var blueMask = (int)(flags & 0x000f);
b = ExpandBits(blueMask, 4);

var alphaMask = (int)( (flags & 0x7000) >> 12);
a = ExpandBits(alphaMask, 3);
}

return new(r, g, b, a);
}

// Get ColorB

private static TextureColor GetColorB(uint flags)
{
bool isOpaque = (flags & 0x80000000) != 0;

byte r, g, b, a;

if(isOpaque)
{
var redMask = (int)( (flags & 0x7c000000) >> 26);
r = ExpandBits(redMask, 5);
	
var greenMask = (int)( (flags & 0x03e00000) >> 21);
g = ExpandBits(greenMask, 5);

var blueMask = (int)( (flags & 0x001f0000) >> 16);	
b = ExpandBits(blueMask, 5);

a = 255;
}

else
{
var redMask = (int)( (flags & 0x0f000000) >> 24);
r = ExpandBits(redMask, 4);

var greenMask = (int)( (flags & 0x00f00000) >> 20);
g = ExpandBits(greenMask, 4);

var blueMask = (int)( (flags & 0x000f0000) >> 16);
b = ExpandBits(blueMask, 4);

var alphaMask = (int)( (flags & 0x70000000) >> 28);
a = ExpandBits(alphaMask, 3);
}

return new(r, g, b, a);
}

// Extract Color as is (no interpolation)

private static TextureColor ExtractColor(in TextureColor colorA, in TextureColor colorB, uint modValue,
                                         bool useAlpha)
{

return modValue switch
{
1 => TextureHelper.InterpolateColors(colorA, colorB, 5, 3, useAlpha),
2 => TextureHelper.InterpolateColors(colorA, colorB, 3, 5, useAlpha),
3 => colorB,
_ => colorA,
};

}

// Get Color Interpolated

private static TextureColor GetInterpolated(in TextureColor colorA, in TextureColor colorB, uint modValue,
                                            bool useAlpha)
{

return modValue switch
{
1 => TextureHelper.InterpolateColors(colorA, colorB, 3, 1, useAlpha),
2 => TextureHelper.InterpolateColors(colorA, colorB, 2, 2, useAlpha),
3 => TextureHelper.InterpolateColors(colorA, colorB, 1, 3, useAlpha),
_ => colorA,
};

}

// Interpolate Colors

private static TextureColor InterpColors(in TextureColor colorA, in TextureColor colorB, uint modValue,
                                         uint mode, bool useAlpha)
{

if(mode == 0)
return ExtractColor(colorA, colorB, modValue, useAlpha);

return GetInterpolated(colorA, colorB, modValue, useAlpha);
}

// Apply Modulation

private static void ApplyMod(uint data, int x, int y, bool is2BPP, out uint modValue, out uint mode)
{
uint mask;
int offset;

if(is2BPP)
{
mode = (data >> 25) & 1;

offset = 2 * (x & 3) + 8 * (y & 3);
mask = 0x3;
}

else
{
mode = (data >> 29) & 1;
offset = 4 * (x & 3) + 16 * (y & 3);

mask = 0xF;
}

modValue = (data >> offset) & mask;
}

// Get Word Index

private static int GetWordIndex(ReadOnlySpan<int> table, int words) => table[1] * words + table[0];

// Apply mod to color

private static int ModColor(int flags, uint mod) => (flags * (int)mod) >> 2;

// Get Color Modulation

private static TextureColor16 GetColorMod(in TextureColor16 baseColor, uint mod)
{
int r = ModColor(baseColor.Red, mod);
int g = ModColor(baseColor.Green, mod);
int b = ModColor(baseColor.Blue, mod);
int a = ModColor(baseColor.Alpha, mod);

return new(r, g, b, a);
}

// Decode PVR Color

private static void DecodeColor(ReadOnlySpan<PVRWord> encoded, TextureColor* plain, int width, int height,
                                bool is2BPP, bool useAlpha)
{
int wordWidth = is2BPP ? 8 : 4;

int blocksX = width / wordWidth;
int blocksY = height / WORD_HEIGHT;

for(int row = 0; row < blocksY; row++)
{

for(int col = 0; col < blocksX; col++)
{
int wordIndex = row * blocksX + col;

var word = encoded[wordIndex];
var flags = word.Flags;

var colorA = GetColorA(flags);
var colorB = GetColorB(flags);

for(int y = 0; y < WORD_HEIGHT; y++)
{

for(int x = 0; x < wordWidth; x++)
{
ApplyMod(word.ModulationData, x, y, is2BPP, out uint modValue, out uint mode);

var finalColor = InterpColors(colorA, colorB, modValue, mode, useAlpha);
int pxOffset = (row * WORD_HEIGHT + y) * width + col * wordWidth + x;  

plain[pxOffset] = finalColor;
}

}

}

}

}

// Sort PVR Words from Morton to Linear

private static void SortWords(Span<PVRWord> words, int blocksX, int blocksY)
{
using NativeMemoryOwner<PVRWord> lOwner = new(words.Length);
var linearWords = lOwner.AsSpan();

for(int row = 0; row < blocksY; row++)
	
for(int col = 0; col < blocksX; col++)
{
int mortonIdx = Morton.GetIndex(col, row);
int linearIdx = row * blocksX + col;

linearWords[linearIdx] = words[mortonIdx];
}

linearWords.CopyTo(words);
}

// Decode PVR (Internal)

private static SKBitmap Decode(Stream reader, int width, int height, Endianness endian,
							   bool is2BPP, bool useAlpha)
{
AdjustSize(ref width, ref height, is2BPP);

SKBitmap image = new(width, height);
var pixels = (TextureColor*)image.GetPixels().ToPointer();

TraceLogger.WriteActionStart("Reading raw data...");

int blockWidth = is2BPP ? 8 : 4;

int blocksX = width / blockWidth;
int blocksY = height / WORD_HEIGHT;

int totalBlocks = blocksX * blocksY;

int bufferSize = totalBlocks * 8;
using var rOwner = reader.ReadPtr(bufferSize, endian);

var rawBytes = rOwner.AsSpan();
var words = MemoryMarshal.Cast<byte, PVRWord>(rawBytes);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Sorting blocks...");
SortWords(words, blocksX, blocksY);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writing pixels...");
DecodeColor(words, pixels, width, height, is2BPP, useAlpha);

TraceLogger.WriteActionEnd();

return image;
}

// Decode PVR (2BPP)

public static SKBitmap Decode2bpp(Stream reader, int width, int height, Endianness endian = default)
{
return Decode(reader, width, height, endian, true, true);
}

// Decode PVR (4BPP)

public static SKBitmap Decode4bpp(Stream reader, int width, int height, bool useAlpha,
                                  Endianness endian = default)
{
return Decode(reader, width, height, endian, false, useAlpha);
}

#endregion
}