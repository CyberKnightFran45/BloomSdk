﻿using System.IO;
using SkiaSharp;

// Parse DXT5 Images in RGBA Order

public static class DXT5_RGBA
{
// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• DXT5-RGBA Texture Decode:");
TraceLogger.WriteLine();

return DXT.Decode(reader, width, height, endian, DXT3_RGBA.DecodeBlock, DXT4_RGBA.DecodeAlpha);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• DXT5-RGBA Texture Encode:");
TraceLogger.WriteLine();

return DXT.Encode(writer, image, endian, false, null, DXT4_RGBA.EncodeAlpha);
}

}