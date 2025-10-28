using System.IO;
using SkiaSharp;

public static class RGBA5551
{
// Unpack Bits from Mask

private static byte UnpackBits(int mask) => (byte)( (mask << 3) | (mask >> 2) );

// Get Color from RGBA551

internal static TextureColor DecodeColor(ushort flags)
{
int redMask = (flags & 0xF800) >> 11;
int greenMask = (flags & 0x7C0) >> 6;
int blueMask = (flags & 0x3E) >> 1;

byte r = UnpackBits(redMask);
byte g = UnpackBits(greenMask);
byte b = UnpackBits(blueMask);
var a = (byte)-(flags & 0x1);

return new(r, g, b, a);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• RGBA-5551 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

internal static ushort EncodeColor(TextureColor color)
{
int r = (color.Red & 0xF8) << 8;
int g = (color.Green & 0xF8) << 3;
int b = (color.Blue & 0xF8) >> 2;
int a = (color.Alpha & 0x80) >> 7;

return (ushort)(r | g | b | a);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• RGBA-5551 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, endian, EncodeColor);
}

}