using System.IO;
using SkiaSharp;

// Parse DXT1 Images in RGBA Order

public static class DXT1_RGBA
{
// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• DXT1-RGBA Texture Decode:");
TraceLogger.WriteLine();

return DXT.Decode(reader, width, height, endian);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• DXT1-RGBA Texture Encode:");
TraceLogger.WriteLine();

return DXT.Encode(writer, image, endian, true);
}

}