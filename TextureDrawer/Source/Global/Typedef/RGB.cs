using System;
using System.IO;
using System.Runtime.InteropServices;
using SkiaSharp;

// Supports Linear Textures in RGB format

public static class RGB
{
#region ==========  ENCODER  ==========

// Encode Image as Binary Texture

private static unsafe int Encode<T>(Stream writer, SKBitmap image, Endianness endian,
                                    Func<TextureColor, T> encodeFunc)
									where T : unmanaged
{
var pixels = (TextureColor*)image.GetPixels().ToPointer();
int square = image.GetSquare();

TraceLogger.WriteActionStart("Reading pixels...");

using NativeMemoryOwner<T> cOwner = new(square);
var colorInfo = cOwner.AsSpan();

for(int i = 0; i < square; i++)
colorInfo[i] = encodeFunc(pixels[i]);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writing raw data...");

var rawBytes = MemoryMarshal.AsBytes(colorInfo);
writer.WriteBytes(rawBytes, endian);

TraceLogger.WriteActionEnd();

return image.Width * sizeof(T);
}

// Encode RGB 8-bits

public static int Encode8(Stream writer, SKBitmap image, Func<TextureColor, byte> encodeFunc)
{
return Encode(writer, image, default, encodeFunc);
}

// Encode RGB 16-bits

public static int Encode16(Stream writer, SKBitmap image, Endianness endian,
                           Func<TextureColor, ushort> encodeFunc)
{
return Encode(writer, image, endian, encodeFunc);
}

// Encode Helper for 24-bits

private static Func<TextureColor, uint> EncodeFunc24(Func<TextureColor, uint> baseFunc)
{
return c => baseFunc(c) & 0x00FFFFFF;
}

// Encode RGB 24-bits

public static int Encode24(Stream writer, SKBitmap image, Endianness endian,
                           Func<TextureColor, uint> encodeFunc)
{
return Encode(writer, image, endian, EncodeFunc24(encodeFunc) );
}

// Encode RGB 32-bits

public static int Encode32(Stream writer, SKBitmap image, Endianness endian,
                           Func<TextureColor, uint> encodeFunc)
{
return Encode(writer, image, endian, encodeFunc);
}

// Encode RGB 64-bits

public static int Encode64(Stream writer, SKBitmap image, Endianness endian,
                           Func<TextureColor, ulong> encodeFunc)
{
return Encode(writer, image, endian, encodeFunc);
}

// Encode Tiled Image

private static unsafe int EncodeTile<T>(Stream writer, SKBitmap image, int tileSize,
                                        Endianness endian, Func<TextureColor, T> encodeFunc)
									    where T : unmanaged
{
int width = image.Width;
int height = image.Height;

int newWidth = (width + (tileSize - 1)) & ~(tileSize - 1);
var pixels = (TextureColor*)image.GetPixels().ToPointer();

TraceLogger.WriteActionStart("Reading pixels...");

int totalPixels = newWidth * height;
using NativeMemoryOwner<T> cOwner = new(totalPixels);

var colorInfo = cOwner.AsSpan();
colorInfo.Clear();

int blocksPerRow = (newWidth + tileSize - 1) / tileSize;

for(int i = 0; i < height; i++)
{
int blockY = i / tileSize;
int inBlockY = i % tileSize;

for(int j = 0; j < newWidth; j++)
{
int blockX = j / tileSize;
int inBlockX = j % tileSize;

int blockIndex = blockY * blocksPerRow + blockX;
int pixelIndexInBlock = inBlockY * tileSize + inBlockX;

int dstIndex = blockIndex * tileSize * tileSize + pixelIndexInBlock;

if(j < width)
colorInfo[dstIndex] = encodeFunc(pixels[i * width + j]);

}

}

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writing raw data...");

var rawBytes = MemoryMarshal.AsBytes(colorInfo);
writer.WriteBytes(rawBytes, endian);

TraceLogger.WriteActionEnd();

return newWidth * sizeof(T);
}

// Encode Tile as 16-bits

public static int EncodeTile16(Stream writer, SKBitmap image, int tileSize, Endianness endian,
                               Func<TextureColor, ushort> encodeFunc)
{
return EncodeTile(writer, image, tileSize, endian, encodeFunc);
}

#endregion


#region ==========  DECODER  ==========

// Decode Binary Texture as Png

private static unsafe SKBitmap Decode<T>(Stream reader, int width, int height, Endianness endian,
                                         Func<T, TextureColor> decodeFunc)
									     where T : unmanaged
{
SKBitmap image = new(width, height);

int square = image.GetSquare();
var pixels = (TextureColor*)image.GetPixels().ToPointer();

TraceLogger.WriteActionStart("Reading raw data...");

int bufferSize = square * sizeof(T);
using var rOwner = reader.ReadPtr(bufferSize, endian);

var rawBytes = rOwner.AsSpan();
var colorInfo = MemoryMarshal.Cast<byte, T>(rawBytes);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writing pixels...");

for(int i = 0; i < square; i++)
pixels[i] = decodeFunc(colorInfo[i]);

TraceLogger.WriteActionEnd();

return image;
}

// Decode RGB 8-bits

public static SKBitmap Decode8(Stream reader, int width, int height, Func<byte, TextureColor> decodeFunc)
{
return Decode(reader, width, height, default, decodeFunc);
}

// Decode RGB 16-bits

public static SKBitmap Decode16(Stream reader, int width, int height, Endianness endian,
                                Func<ushort, TextureColor> decodeFunc)
{
return Decode(reader, width, height, endian, decodeFunc);
}

// Decode Helper for 24-bits

private static Func<uint, TextureColor> DecodeFunc24(Func<uint, TextureColor> baseFunc)
{
return f => baseFunc(f & 0x00FFFFFF);
}

// Decode RGB 24-bits

public static SKBitmap Decode24(Stream reader, int width, int height, Endianness endian,
                                Func<uint, TextureColor> decodeFunc)
{
return Decode(reader, width, height, endian, DecodeFunc24(decodeFunc) );
}

// Decode RGB 32-bits

public static SKBitmap Decode32(Stream reader, int width, int height, Endianness endian,
                                Func<uint, TextureColor> decodeFunc)
{
return Decode(reader, width, height, endian, decodeFunc);
}

// Decode RGB 64-bits

public static SKBitmap Decode64(Stream reader, int width, int height, Endianness endian,
                                Func<ulong, TextureColor> decodeFunc)
{
return Decode(reader, width, height, endian, decodeFunc);
}

// Decode Tiled Texture as Png

private static unsafe SKBitmap DecodeTile<T>(Stream reader, int width, int height, int tileSize,
                                             Endianness endian, Func<T, TextureColor> decodeFunc)
											 where T : unmanaged
{
SKBitmap image = new(width, height);
var pixels = (TextureColor*)image.GetPixels().ToPointer();

TraceLogger.WriteActionStart("Reading raw data...");

int bufferSize = image.GetSquare() * sizeof(T);
using var rOwner = reader.ReadPtr(bufferSize, endian);

var rawBytes = rOwner.AsSpan();
var colorInfo = MemoryMarshal.Cast<byte, T>(rawBytes);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writing pixels...");

int blocksPerRow = (width + tileSize - 1) / tileSize;

for(int i = 0; i < height; i++)
{
int blockY = i / tileSize;
int inBlockY = i % tileSize;

for(int j = 0; j < width; j++)
{
int blockX = j / tileSize;
int inBlockX = j % tileSize;

int blockIndex = blockY * blocksPerRow + blockX;
int pixelIndexInBlock = inBlockY * tileSize + inBlockX;
int srcIndex = blockIndex * tileSize * tileSize + pixelIndexInBlock;

pixels[i * width + j] = decodeFunc(colorInfo[srcIndex]);
}

}

TraceLogger.WriteActionEnd();

return image;
}

// Decode Tile (16-bits)

public static SKBitmap DecodeTile16(Stream reader, int width, int height, int tileSize,
                                    Endianness endian, Func<ushort, TextureColor> decodeFunc)
{
return DecodeTile(reader, width, height, tileSize, endian, decodeFunc);
}

#endregion
}