using System;
using System.IO;
using System.Runtime.InteropServices;
using SkiaSharp;

// Supports DirectX Texture Compression (DXT)

public static unsafe class DXT
{
// Tile Size (4x4)

private const int TILE_SIZE = 4;

#region ==========  ENCODER  ==========

// Encode block

public delegate void DXTEncoder(Span<TextureColor> block);

// Write alpha

public delegate void DXTAlphaEncoder(ReadOnlySpan<TextureColor> block, Span<ushort> alpha);

// Convert Color to 565 bits

private static ushort ColorTo565(in TextureColor color)
{
var r = (color.Red >> 3) << 11;
var g = (color.Green >> 2) << 5;
var b = color.Blue >> 3;

return (ushort)(r | g | b);
}

// Swap Colors

private static void SwapColors(ref TextureColor c1, ref TextureColor c2) => (c2, c1) = (c1, c2);

// Cuadratic Pow

private static int Pow(int x) => x * x;

// Use euclidean distance

private static int GetDistance(in TextureColor c1, in TextureColor c2)
{
var rDiff = Pow(c1.Red - c2.Red);
var gDiff = Pow(c1.Green - c2.Green);
var bDiff = Pow(c1.Blue - c2.Blue);

return rDiff + gDiff + bDiff;
}

// Euclidan distance for Color range

private static void EuclideanDistance(ReadOnlySpan<TextureColor> pixels, bool alphaEndpoints,
                                      out TextureColor min, out TextureColor max)
{
max = min = default;

int maxDistance = -1;

for(int i = 0; i < 15; i++)
{
var px = pixels[i];

if(alphaEndpoints && px.Alpha <= 128)
continue;

for(int j = i + 1; j < 16; j++)
{
var px2 = pixels[j];

if(alphaEndpoints && px2.Alpha <= 128)
continue;

int distance = GetDistance(px, px2);

if(distance > maxDistance)
{
maxDistance = distance;

min = px;
max = px2;
}

}

}

ushort min565 = ColorTo565(min);
ushort max565 = ColorTo565(max);

if(max565 < min565)
SwapColors(ref min, ref max);

}

// Combine Colors

private static int CombineColors(in TextureColor color, int r, int g, int b)
{
int rDiff = Math.Abs(r - color.Red);
int gDiff = Math.Abs(g - color.Green);
int bDiff = Math.Abs(b - color.Blue);

return rDiff + gDiff + bDiff;
}

// Interpolate (Variant A)

private static int InterpA(int a, int b, bool alphaEndpoints)
{
return alphaEndpoints ? (a + b) >> 1 : ( (a << 1) + b) / 3;
}

// Interpolate (Variant B)

private static int InterpB(int a, int b, bool alphaEndpoints) => alphaEndpoints ? 0 : (a + (b << 1) ) / 3; 

// Emit ColorIndices (only varies for DXT1-RGBA)

private static int EmitColorIndices(ReadOnlySpan<TextureColor> pixels, in TextureColor min,
                                    in TextureColor max, bool alphaEndpoints)
{
Span<int> colors = stackalloc int[16];
int indices = 0;

// Base colors

int r0 = (max.Red & 0xF8) | (max.Red >> 5);
int g0 = (max.Green & 0xFC) | (max.Green >> 6);
int b0 = (max.Blue & 0xF8) | (max.Blue >> 5);

int r1 = (min.Red & 0xF8) | (min.Red >> 5);
int g1 = (min.Green & 0xFC) | (min.Green >> 6);
int b1 = (min.Blue & 0xF8) | (min.Blue >> 5);

colors[0] = r0;
colors[1] = g0;
colors[2] = b0;
colors[4] = r1;
colors[5] = g1;
colors[6] = b1;

// Interpolations

int r2 = InterpA(r0, r1, alphaEndpoints);
int g2 = InterpA(g0, g1, alphaEndpoints);
int b2 = InterpA(b0, b1, alphaEndpoints);

int r3 = InterpB(r0, r1, alphaEndpoints);
int g3 = InterpB(g0, g1, alphaEndpoints);
int b3 = InterpB(b0, b1, alphaEndpoints);

colors[8] = r2;
colors[9] = g2;
colors[10] = b2;
colors[12] = r3;
colors[13] = g3;
colors[14] = b3;

for(int i = 15; i >= 0; i--)
{
var px = pixels[i];

if(alphaEndpoints && px.Alpha < 128)
{
indices |= (0b11) << (i << 1);

continue;
}

int d0 = CombineColors(px, r0, g0, b0);
int d1 = CombineColors(px, r1, g1, b1);
int d2 = CombineColors(px, r2, g2, b2);
int d3 = CombineColors(px, r3, g3, b3);

if(alphaEndpoints)
{
int mask = d0 > d2 && d1 > d2 ? 0b10 : 0b01;

indices |= (mask) << (i << 1);
}

else
{
int p0 = d0 > d3 ? 1 : 0;
int p1 = d1 > d2 ? 1 : 0;
int p2 = d0 > d2 ? 1 : 0;
int p3 = d1 > d3 ? 1 : 0;
int p4 = d2 > d3 ? 1 : 0;

int x0 = p1 & p2;
int x1 = p0 & p3;
int x2 = p0 & p4;

indices |= (x2 | ( (x0 | x1) << 1) ) << (i << 1);
}

}

return indices;
}

// Encode DXT

public static int Encode(Stream writer, SKBitmap image, Endianness endian, bool alphaEndpoints,
                         DXTEncoder blockEncoder = null, DXTAlphaEncoder alphaFunc = null)
{
var pixels = (TextureColor*)image.GetPixels().ToPointer();
 
int width  = image.Width;
int height = image.Height;

bool useAlpha = alphaFunc != null;

TraceLogger.WriteActionStart("Reading pixels...");

int blocksPerRow = TextureHelper.GetBlockDim(width, TILE_SIZE);
int blocksPerCol = TextureHelper.GetBlockDim(height, TILE_SIZE);

int totalBlocks = blocksPerRow * blocksPerCol;
int blockSize = useAlpha ? 8 : 4;

int bufferSize = totalBlocks * blockSize;

using NativeMemoryOwner<ushort> cOwner = new(bufferSize);
var colorInfo = cOwner.AsSpan();

Span<TextureColor> block = stackalloc TextureColor[16];
int colorOffset = useAlpha ? 4 : 0;

for(int blockY = 0; blockY < blocksPerCol; blockY++)
{

for(int blockX = 0; blockX < blocksPerRow; blockX++)
{
int blockIndex = blockY * blocksPerRow + blockX;
var outBlock = colorInfo.Slice(blockIndex * blockSize, blockSize);

for(int i = 0; i < TILE_SIZE; i++)

for(int j = 0; j < TILE_SIZE; j++)
{
int srcX = blockX * TILE_SIZE + j;
int srcY = blockY * TILE_SIZE + i;

bool shouldCopy = srcX < width && srcY < height;

block[(i << 2) | j] = shouldCopy ? pixels[srcY * width + srcX] : default;
}

blockEncoder?.Invoke(block);

if(useAlpha)
alphaFunc.Invoke(block, outBlock[.. 4] );

EuclideanDistance(block, alphaEndpoints, out var min, out var max);

int indices = EmitColorIndices(block, min, max, alphaEndpoints);

var c0 = alphaEndpoints ? ColorTo565(min) : ColorTo565(max);
var c1 = alphaEndpoints ? ColorTo565(max) : ColorTo565(min);
var f0 = (ushort)(indices & 0xFFFF);
var f1 = (ushort)(indices >> 16);

outBlock[colorOffset] = c0;
outBlock[colorOffset + 1] = c1;
outBlock[colorOffset + 2] = f0;
outBlock[colorOffset + 3] = f1;
}

}

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writing raw data...");

var rawBytes = MemoryMarshal.AsBytes(colorInfo);
writer.WriteBytes(rawBytes, endian);

TraceLogger.WriteActionEnd();

return blocksPerRow;
}

#endregion


#region ==========  DECODER  ==========

// Block decoder

public delegate void DXTDecoder(ReadOnlySpan<byte> alpha, Span<TextureColor> block);

// Alpha decoder (only used in DXT3-5)

public delegate void DXTAlphaDecoder(ReadOnlySpan<ushort> encoded, Span<byte> plain);

// Unpack bits

private static byte UnpackBits(int flags, int left, int right) => (byte)( (flags << left) | (flags >> right) );

// Decode Color from 565-bits

private static TextureColor ColorFrom565(ushort flags)
{
var r = (byte)( (flags >> 11) & 0x1F);
var g = (byte)( (flags >> 5) & 0x3F);
var b = (byte)(flags & 0x1F);

byte dR = UnpackBits(r, 3, 2);
byte dG = UnpackBits(g, 2, 4);
byte dB = UnpackBits(b, 3, 2);

return new(dR, dG, dB);
}

// Compute Colors

private static void ComputeColors(ushort c0, ushort c1, Span<TextureColor> palette, bool hasAlpha)
{
bool shouldInterpolate = hasAlpha || c0 > c1;

var d0 = ColorFrom565(c0);
var d1 = ColorFrom565(c1);

TextureColor d2, d3;

if(shouldInterpolate)
{
d2 = TextureHelper.InterpolateColors(d0, d1, 2, 1, false);
d3 = TextureHelper.InterpolateColors(d0, d1, 1, 2, false);
}

else
{
d2 = TextureHelper.InterpolateColors(d0, d1, 1, 1, false);
d3 = default;
}

palette[0] = d0;
palette[1] = d1;
palette[2] = d2;
palette[3] = d3;
}

// Decode ColorIndices

private static void DecodeIndices(ReadOnlySpan<byte> indices, ReadOnlySpan<TextureColor> palette,
                                  ReadOnlySpan<byte> alphas, Span<TextureColor> block)
{

for(int i = 0; i < TILE_SIZE; i++)
{
byte row = indices[i];

for(int j = 0; j < TILE_SIZE; j++)
{
int blockIndex = (i << 2) | j;
int colorIndex = row & 0b11;

block[blockIndex] = palette[colorIndex];
row >>= 2;
}

}

}


// Decode DXT

public static SKBitmap Decode(Stream reader, int width, int height, Endianness endian,
                              DXTDecoder blockDecoder = null, DXTAlphaDecoder alphaFunc = null)
{
SKBitmap image = new(width, height);

var pixels = (TextureColor*)image.GetPixels().ToPointer();
bool useAlpha = alphaFunc != null;

TraceLogger.WriteActionStart("Reading raw data...");

int blocksPerRow = TextureHelper.GetBlockDim(width, TILE_SIZE);
int blocksPerCol = TextureHelper.GetBlockDim(height, TILE_SIZE);

int totalBlocks = blocksPerRow * blocksPerCol;
int bytesPerBlock = useAlpha ? 16 : 8;

int bufferSize = totalBlocks * bytesPerBlock;

using var rOwner = reader.ReadPtr(bufferSize, endian);
var rawBytes = rOwner.AsSpan();

TraceLogger.WriteActionEnd();

Span<TextureColor> block = stackalloc TextureColor[16];
Span<TextureColor> palette = stackalloc TextureColor[4];

Span<byte> colorIndices = stackalloc byte[4];
Span<byte> alphas = useAlpha ? stackalloc byte[16] : default;

TraceLogger.WriteActionStart("Writing pixels...");

int colorOffset = useAlpha ? 4 : 0;

for(int blockY = 0; blockY < blocksPerCol; blockY++)
{

for(int blockX = 0; blockX < blocksPerRow; blockX++)
{
int blockIndex = blockY * blocksPerRow + blockX;
var blockData = rawBytes.Slice(blockIndex * bytesPerBlock, bytesPerBlock);

var currBlock = MemoryMarshal.Cast<byte, ushort>(blockData);

if(useAlpha)
alphaFunc(currBlock[.. 4], alphas);

var colorInfo = currBlock.Slice(colorOffset, 4);

ushort c0 = colorInfo[0];
ushort c1 = colorInfo[1];
ushort f0 = colorInfo[2];
ushort f1 = colorInfo[3];

colorIndices[0] = (byte)(f0 & 0xFF);
colorIndices[1] = (byte)(f0 >> 8);
colorIndices[2] = (byte)(f1 & 0xFF);
colorIndices[3] = (byte)(f1 >> 8);

ComputeColors(c0, c1, palette, useAlpha);
DecodeIndices(colorIndices, palette, alphas, block);

blockDecoder?.Invoke(alphas, block);

for(int i = 0; i < TILE_SIZE; i++)
{
	
for(int j = 0; j < TILE_SIZE; j++)
{
int row = blockY * TILE_SIZE + i;
int col = blockX * TILE_SIZE + j;

if(col + j < width && row + i < height)
pixels[row * width + col] = block[(i << 2) | j];

}

}

}

}

TraceLogger.WriteActionEnd();

return image;
}

#endregion
}