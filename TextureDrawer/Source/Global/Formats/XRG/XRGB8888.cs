using System.IO;
using SkiaSharp;

public static class XRGB8888
{
// Get Color from Binary

internal static TextureColor DecodeColor(uint flags)
{
var r = (byte)( (flags & 0xFF0000) >> 16);
var g = (byte)( (flags & 0xFF00) >> 8);
var b = (byte)(flags & 0xFF);

return new(r, g, b);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• XRGB-8888 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode32(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

internal static uint EncodeColor(TextureColor color)
{
var r = (uint)(color.Red << 16);
var g = (uint)(color.Green << 8);
uint b = color.Blue;

return 0xFF000000 | r | g | b;
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• XRGB-8888 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode32(writer, image, endian, EncodeColor);
}

}