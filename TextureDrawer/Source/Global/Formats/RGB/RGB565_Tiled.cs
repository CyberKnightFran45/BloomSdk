using System.IO;
using SkiaSharp;

// Parse Image in the RGB565 format (32x32 Tiled)

public static unsafe class RGB565_Tiled
{
// Tile Size

private const int TILE_SIZE = 32;
 
// Get Color from RGB565 (Tiled)

private static TextureColor DecodeColor(ushort flags)
{
var r = (byte)( (flags & 0xF800) >> 8);
var g = (byte)( (flags & 0x7E0) >> 3);
var b = (byte)( (flags & 0x1F) << 3);

return new(r, g, b);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• Tiled-RGB-565 Texture Decode:");
TraceLogger.WriteLine();

return RGB.DecodeTile16(reader, width, height, TILE_SIZE, endian, DecodeColor);
}

// Get Color from Image

private static ushort EncodeColor(TextureColor color)
{
int r = (color.Red & 0xF8) << 8;
int g = (color.Green & 0xFC) << 3;
int b = (color.Blue & 0xF8) >> 3;

return (ushort)(r | g | b);
}

// Write pixels

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• Tiled-RGB-565 Texture Encode:");
TraceLogger.WriteLine();

return RGB.EncodeTile16(writer, image, TILE_SIZE, endian, EncodeColor);
}

}