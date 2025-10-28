﻿using System.IO;
using SkiaSharp;

// Parse ETC1-RGB Images followed by A8

public static class ETC1_RGB_A8
{
// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• ETC1-RGB + A8 Texture Decode:");
TraceLogger.WriteLine();

var decImg = ETC1.Decode(reader, width, height, endian);
AlphaCodec.Decode8(reader, decImg);

return decImg;
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• ETC1-RGB + A8 Texture Encode:");
TraceLogger.WriteLine();

_ = ETC1.Encode(writer, image, endian);
AlphaCodec.Encode8(writer, image);

return image.Width * 4;
}

}