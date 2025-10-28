﻿using System.IO;
using SkiaSharp;

// Parse ETC1-RGB Images followed by Alpha Palette

public static class ETC1_RGB_A_Palette
{
// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• ETC1-RGB + A-Palette Texture Decode:");
TraceLogger.WriteLine();

var decImg = ETC1.Decode(reader, width, height, endian);
AlphaCodec.DecodePalette4(reader, decImg);

return decImg;
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, out int paletteSize, Endianness endian = default)
{
TraceLogger.WriteLine("• ETC1-RGB + A-Palette Texture Encode:");
TraceLogger.WriteLine();

_ = ETC1.Encode(writer, image, endian);
paletteSize = AlphaCodec.EncodePalette4(writer, image);

return image.Width * 4;
}

}