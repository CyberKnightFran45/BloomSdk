using System.IO;
using SkiaSharp;

public static class RGB565
{
// Get Color from RGB565

private static TextureColor DecodeColor(ushort flags)
{
int redMask = flags >> 11;
int greenMask = (flags & 0x7E0) >> 5;
int blueMask = flags & 0x1F;

var r = (byte)( (redMask << 3) | (redMask >> 2) );
var g = (byte)( (greenMask << 2) | (greenMask >> 4) );
var b = (byte)( (blueMask << 3) | (blueMask >> 2) );

return new(r, g, b);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• RGB-565 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

private static ushort EncodeColor(TextureColor color)
{
int r = (color.Red & 0xF8) << 8;
int g = (color.Green & 0xFC) << 3;
int b = color.Blue >> 3;

return (ushort)(r | g | b);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• RGB-565 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, endian, EncodeColor);
}

}