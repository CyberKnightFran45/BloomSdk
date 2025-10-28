using System.IO;
using SkiaSharp;

// Parse Image in the ARGB format (8 bits per Color)

public static class ARGB8888
{
// Get Color from Binary

internal static TextureColor DecodeColor(uint flags)
{
var r = (byte)( (flags & 0xFF0000) >> 16);
var g = (byte)( (flags & 0xFF00) >> 8);
var b = (byte)(flags & 0xFF);
var a = (byte)(flags >> 24);

return new(r, g, b, a);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• ARGB-8888 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode32(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

internal static uint EncodeColor(TextureColor color)
{
int a = color.Alpha << 24;
int r = color.Red << 16;
int g = color.Green << 8;
int b = color.Blue;

return (uint)(a | r | g | b);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• ARGB-8888 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode32(writer, image, endian, EncodeColor);
}

}