﻿using System.IO;
using SkiaSharp;

// Parse PVR-RGB Images followed by A8 (4bpp)

public static class PVRTC_4BPP_RGB_A8
{
// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• PVRTC-4BPP-RGB + A8 Texture Decode:");
TraceLogger.WriteLine();

var decImg = PVR.Decode4bpp(reader, width, height, false, endian);
AlphaCodec.Decode8(reader, decImg);

return decImg;
}

// Write pixels to Bitmap

public static int Write(Stream writer, ref SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• PVRTC-4BPP-RGB + A8 Texture Encode:");
TraceLogger.WriteLine();

_ = PVR.Encode4bpp(writer, ref image, false, endian);
AlphaCodec.Encode8(writer, image);

return image.Width * 4;
}

}