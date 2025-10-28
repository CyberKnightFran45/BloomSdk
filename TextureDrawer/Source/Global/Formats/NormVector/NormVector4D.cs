using System.IO;
using SkiaSharp;

// Normalize Bytes for 4D Vectors 

public static class NormVector4D
{
// Get Color

private static TextureColor DecodeColor(uint flags)
{
var r = (byte)( (flags & 0xFF000000) >> 24);
var g = (byte)( (flags & 0x00FF0000) >> 16);
var b = (byte)( (flags & 0x0000FF00) >> 8);
var a = (byte)(flags & 0x000000FF);

return new(r, g, b, a);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height)
{
TraceLogger.WriteLine("• Normalized Vector4D Decode:");
TraceLogger.WriteLine();

return RGB.Decode32(reader, width, height, default, DecodeColor);
}

// Encode Color

private static uint EncodeColor(TextureColor color)
{
int r = color.Red << 24;
int g = color.Green << 16;
int b = color.Blue << 8;
int a = color.Alpha;

return (uint)(r | g | b | a);
}

// Write vector

public static int Write(Stream writer, SKBitmap image)
{
TraceLogger.WriteLine("• Normalized Vector4D Encode:");
TraceLogger.WriteLine();

return RGB.Encode32(writer, image, default, EncodeColor);
}

}