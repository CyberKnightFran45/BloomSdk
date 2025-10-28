using System.IO;
using SkiaSharp;

// Parse Images with ARGB 1555

public static class ARGB1555
{
// Get Color from ARGB1555

private static TextureColor DecodeColor(ushort flags)
{
int redMask = (flags & 0x7C00) >> 10;
int greenMask = (flags & 0x3E0) >> 5;
int blueMask = flags & 0x1F;

var r = (byte)( (redMask << 3) | (redMask >> 2) );
var g = (byte)( (greenMask << 3) | (greenMask >> 2) );
var b = (byte)( (blueMask << 3) | (blueMask >> 2) );
var a = (byte)( (flags & 0x8000) != 0 ? 255 : 0);

return new(r, g, b, a);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• ARGB-1555 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

private static ushort EncodeColor(TextureColor color)
{
int a = (color.Alpha & 0x80) << 8;
int r = (color.Red & 0xF8) << 7;
int g = (color.Green & 0xF8) << 2;
int b = color.Blue >> 3;

return (ushort)(a | r | g | b);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• ARGB-1555 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, endian, EncodeColor);
}

}