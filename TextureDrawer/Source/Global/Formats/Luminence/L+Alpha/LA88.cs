using System.IO;
using SkiaSharp;

// Parse Luminance + Alpha Images (16 bits)

public static class LA88
{
// Decode Contrast

private static TextureColor DecodeContrast(ushort flags)
{
var l = (byte)(flags >> 8);
var a = (byte)(flags & 0xFF);

return new(l, l, l, a);
}

// Read LA88 Texture

public static SKBitmap Read(Stream reader, int width, int height, Endianness endian = default)
{
TraceLogger.WriteLine("• LA88 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode16(reader, width, height, endian, DecodeContrast);
}

// Encode Contrast

private static ushort EncodeContrast(TextureColor color)
{
var lumi = (byte)(L8.EncodeLuminance(color) << 8);

return (ushort)(lumi | color.Alpha);
}

// Write LA88 Texture

public static int Write(Stream writer, SKBitmap image, Endianness endian = default)
{
TraceLogger.WriteLine("• LA88 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode16(writer, image, endian, EncodeContrast);
}

}