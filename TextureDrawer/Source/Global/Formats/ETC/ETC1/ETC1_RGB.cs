using System.IO;
using SkiaSharp;

// Parse ETC1 Images in RGB Mode

public static class ETC1_RGB
{
// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• ETC1-RGB Texture Decode:");
TraceLogger.WriteLine();

return ETC1.Decode(reader, width, height, endian);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• ETC1-RGB Texture Encode:");
TraceLogger.WriteLine();

return ETC1.Encode(writer, image, endian);
}

}