using System.IO;
using SkiaSharp;

public static class XRGB888
{
// Get Color from Binary

private static TextureColor DecodeColor(uint flags)
{
var r = (byte)( (flags & 0xFF0000) >> 16);
var g = (byte)( (flags & 0x00FF00) >> 8);
var b = (byte)(flags & 0x0000FF);

return new(r, g, b);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• XRGB-888 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode24(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

private static uint EncodeColor(TextureColor color)
{
var r = (uint)(color.Red << 16);
var g = (uint)(color.Green << 8);
var b = color.Blue;

return r | g | b;
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• XRGB-888 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode24(writer, image, endian, EncodeColor);
}

}