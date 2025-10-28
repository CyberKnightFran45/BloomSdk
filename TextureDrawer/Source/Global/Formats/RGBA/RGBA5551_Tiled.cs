using System.IO;
using SkiaSharp;

// Parse Image in the RGBA5551 format (32x32 Tiled)

public static class RGBA5551_Tiled
{
// Tile Size

private const int TILE_SIZE = 32;

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• Tiled-RGBA-5551 Texture Decode:");
TraceLogger.WriteLine();

return RGB.DecodeTile16(reader, width, height, TILE_SIZE, endian, RGBA5551.DecodeColor);
}

// Write pixels

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• Tiled-RGBA-5551 Texture Encode:");
TraceLogger.WriteLine();

return RGB.EncodeTile16(writer, image, TILE_SIZE, endian, RGBA5551.EncodeColor);
}

}