using System.IO;
using SkiaSharp;

// Parse Luminance + Alpha Images (8 bits)

public static class LA44
{
// Expand Bits

private static byte ExpandBits(int flags) => (byte)(flags | (flags << 4) );

// Decode Contrast

private static TextureColor DecodeContrast(byte flags)
{
byte a = ExpandBits(flags & 0xF);
byte l = ExpandBits(flags >> 4);

return new(l, l, l, a);
}

// Read LA44 Texture

public static SKBitmap Read(Stream reader, int width, int height)
{
TraceLogger.WriteLine("• LA44 Texture Decode:");
TraceLogger.WriteLine();

return RGB.Decode8(reader, width, height, DecodeContrast);
}

// Encode Contrast

private static byte EncodeContrast(TextureColor color)
{
var lumi = (byte)(L8.EncodeLuminance(color) & 0xF0);
var alpha = (byte)(color.Alpha >> 4);

return (byte)(lumi | alpha);
}

// Write LA44 Texture

public static int Write(Stream writer, SKBitmap image)
{
TraceLogger.WriteLine("• LA44 Texture Encode:");
TraceLogger.WriteLine();

return RGB.Encode8(writer, image, EncodeContrast);
}

}