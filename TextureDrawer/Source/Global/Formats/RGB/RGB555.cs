using System.IO;
using SkiaSharp;

public static class RGB555
{
// Get Color from RGB555

private static TextureColor DecodeColor(ushort flags)
{
int redMask = (flags >> 10) & 0x1F;
int greenMask = (flags >> 5) & 0x1F;
int blueMask = flags & 0x1F;

var r = (byte)( (redMask << 3) | (redMask >> 2) );
var g = (byte)( (greenMask << 3) | (greenMask >> 2) );
var b = (byte)( (blueMask << 3) | (blueMask >> 2) );

return new(r, g, b);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• RGB-555 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

private static ushort EncodeColor(TextureColor color)
{
int r = (color.Red & 0xF8) << 7;
int g = (color.Green & 0xF8) << 2;
int b = color.Blue >> 3;

return (ushort)(r | g | b);
}

// Write pixels to Bitmap
	
public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• RGB-555 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, endian, EncodeColor);
}

}