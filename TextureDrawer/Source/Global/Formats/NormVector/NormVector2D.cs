using System.IO;
using SkiaSharp;

// Normalize Bytes for 2D Vectors 

public static class NormVector2D
{
// Get Color

private static TextureColor DecodeColor(ushort flags)
{
var r = (byte)( (flags & 0xFF00) >> 8);
var g = (byte)(flags & 0x00FF);

return new(r, g, 0);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height)
{
TraceLogger.WriteLine("• Normalized Vector2D Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, default, DecodeColor);
}

// Encode Color

private static ushort EncodeColor(TextureColor color)
{
var r = (ushort)(color.Red & 0xFF);
var g = (ushort)(color.Green & 0xFF);

return (ushort)( (r << 8) | g);
}

// Write vector

public static int Write(Stream writer, SKBitmap image)
{
TraceLogger.WriteLine("• Normalized Vector2D Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, default, EncodeColor);
}

}