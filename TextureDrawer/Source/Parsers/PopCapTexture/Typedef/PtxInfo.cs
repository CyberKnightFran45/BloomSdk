using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.InteropServices;

namespace TextureDrawer.Parsers.PopCapTexture
{
/// <summary> Stores info related to a Encoded PTX Image (used by the Tool) </summary>

[StructLayout(LayoutKind.Explicit, Size = 28) ]

public readonly struct PtxInfo
{
/** <summary> Gets a Identifier for this Struct </summary>
<returns> The Ptx Identifier. </returns> */

[FieldOffset(0)]
public readonly uint Magic = 0x70747831;

/** <summary> Gets the Texture Width. </summary>
<returns> The TextureWidth. </returns> */

[FieldOffset(4)]
public readonly int Width;

/** <summary> Gets the Texture Height. </summary>
<returns> The TextureHeight. </returns> */

[FieldOffset(8)]
public readonly int Height;

/** <summary> Gets the Texture Pitch. </summary>
<returns> The TexturePitch. </returns> */

[FieldOffset(12)]
public readonly int Pitch;

/** <summary> Gets the Texture Format. </summary>
<returns> The TextureFormat. </returns> */

[FieldOffset(16)]
public readonly PtxFormat Format;

/** <summary> Gets the amount of bytes written in AlphaChannel </summary>
<returns> The AlphaSize. </returns> */

[FieldOffset(20)]
public readonly int AlphaSize;

/** <summary> Gets the type of Alpha used. </summary>
<returns> The AlphaChannel. </returns> */

[FieldOffset(24)]
public readonly PtxAlphaChannel AlphaChannel;

// ctor

public PtxInfo(int width, int height, int pitch, PtxFormat format,
               int alphaSize, PtxAlphaChannel alphaChannel)
{
Width = width;
Height = height;

Pitch = pitch;
Format = format;

AlphaSize = alphaSize;
AlphaChannel = alphaChannel;
}
  
// Read PtxInfo

public static PtxInfo ReadBin(Stream reader)
{
Span<byte> rawData = stackalloc byte[28];
reader.ReadExactly(rawData);

return MemoryMarshal.Read<PtxInfo>(rawData);
}

// Write SexyTexInfo

public readonly void WriteBin(Stream writer, Endianness endian)
{
Span<byte> rawData = stackalloc byte[28];

var info = endian == Endianness.BigEndian ? SwapEndian(this) : this;
MemoryMarshal.Write(rawData, info);

writer.Write(rawData);
}

// Reverse Endianness

public static PtxInfo SwapEndian(in PtxInfo info)
{
int width = BinaryPrimitives.ReverseEndianness(info.Width);
int height = BinaryPrimitives.ReverseEndianness(info.Height);

int pitch = BinaryPrimitives.ReverseEndianness(info.Pitch);
var format = (PtxFormat)BinaryPrimitives.ReverseEndianness( (uint)info.Format);

int alphaSize = BinaryPrimitives.ReverseEndianness(info.AlphaSize);
var alphaChannel = (PtxAlphaChannel)BinaryPrimitives.ReverseEndianness( (uint)info.AlphaChannel);

return new(width, height, pitch, format, alphaSize, alphaChannel);
}

}

}