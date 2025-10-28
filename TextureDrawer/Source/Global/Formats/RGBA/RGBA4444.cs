using System.IO;
using SkiaSharp;

public static class RGBA4444
{
// Unpack Bits from Mask

private static byte UnpackBits(int mask) => (byte)( (mask << 4) | mask);

// Get Color from RGBA4444

internal static TextureColor DecodeColor(ushort flags)
{
int redMask = flags >> 12;
int greenMask = (flags & 0xF00) >> 8;
int blueMask = (flags & 0xF0) >> 4;
int alphaMask = flags & 0xF;

byte r = UnpackBits(redMask);
byte g = UnpackBits(greenMask);
byte b = UnpackBits(blueMask);
byte a = UnpackBits(alphaMask);

return new(r, g, b, a);
}

// Read Bitmap

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• RGBA-4444 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, endian, DecodeColor);
}

// Get Color from Image

internal static ushort EncodeColor(TextureColor color)
{
int r = (color.Red & 0xF0) << 8;
int g = (color.Green & 0xF0) << 4;
int b = color.Blue & 0xF0;
int a = color.Alpha >> 4;

return (ushort)(r | g | b | a);
}

// Write pixels to Bitmap

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• RGBA-4444 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, endian, EncodeColor);
}

}