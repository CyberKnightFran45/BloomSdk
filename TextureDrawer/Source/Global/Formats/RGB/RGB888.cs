using System.IO;
using SkiaSharp;

public static class RGB888
{
// Get Color from RGB888

private static TextureColor DecodeColor(uint flags)
{
var r = (byte)(flags >> 16);
var g = (byte)( (flags & 0xFF00) >> 8);
var b = (byte)(flags & 0xFF);

return new(r, g, b);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• RGB-888 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode24(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

private static uint EncodeColor(TextureColor color)
{
int r = color.Red << 16;
int g = color.Green << 8;
int b = color.Blue;

return (uint)(r | g | b);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• RGB-888 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode24(writer, image, endian, EncodeColor);
}

}