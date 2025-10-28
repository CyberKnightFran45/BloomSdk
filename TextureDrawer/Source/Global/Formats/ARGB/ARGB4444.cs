using System.IO;
using SkiaSharp;

// Parse Image in the ARGB format (4 bits per Color)

public static class ARGB4444
{
// Get Color from Binary

private static TextureColor DecodeColor(ushort flags)
{
int redMask = (flags & 0xF00) >> 8;
int greenMask = (flags & 0xF0) >> 4;
int blueMask = flags & 0xF;
int alphaMask = flags >> 12;

var r = (byte)( (redMask << 4) | redMask);
var g = (byte)( (greenMask << 4) | greenMask);
var b = (byte)( (blueMask << 4) | blueMask);
var a = (byte)( (alphaMask << 4) | alphaMask);

return new(r, g, b, a);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• ARGB-4444 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

private static ushort EncodeColor(TextureColor color)
{
int a = (color.Alpha & 0xF0) << 8;
int r = (color.Red & 0xF0) << 4;
int g = color.Green & 0xF0;
int b = color.Blue >> 4;

return (ushort)(a | r | g | b);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• ARGB-4444 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, endian, EncodeColor);
}

}