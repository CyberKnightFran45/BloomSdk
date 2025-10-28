using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.InteropServices;

namespace TextureDrawer.Parsers.XboxPackedTexture
{
/// <summary> Represents Info for a Xbox360 Packed Texture (PTX). </summary>

[StructLayout(LayoutKind.Explicit, Size = 12) ]

public readonly struct PtxInfo 
{
/** <summary> Gets the Texture Width. </summary>
<returns> The TextureWidth. </returns> */

[FieldOffset(0)]
public readonly int Width;

/** <summary> Gets or Sets the Texture Height. </summary>
<returns> The TextureHeight. </returns> */

[FieldOffset(4)]
public readonly int Height;

/** <summary> Gets or Sets the Block Size with Padding. </summary>
<returns> The BlockSize. </returns> */

[FieldOffset(8)]
public readonly int BlockSize;

/// <summary> Creates a new Instance of the <c>PtxInfo</c>. </summary>

public PtxInfo(int height, int width, int blockSize)
{
Height = height;
Width = width;

BlockSize = blockSize;
}

// Read PtxInfo

public static PtxInfo ReadBin(Stream reader)
{
Span<byte> rawData = stackalloc byte[12];
reader.ReadExactly(rawData);

var info = MemoryMarshal.Read<PtxInfo>(rawData);

return SwapEndian(info);
}

// Write Info to BinaryStream

public readonly void WriteBin(Stream writer)
{
Span<byte> rawData = stackalloc byte[12];

var info = SwapEndian(this);
MemoryMarshal.Write(rawData, info);

writer.Write(rawData);
}

// Reverse Endianness

private static PtxInfo SwapEndian(in PtxInfo info)
{
int width = BinaryPrimitives.ReverseEndianness(info.Width);
int height = BinaryPrimitives.ReverseEndianness(info.Height);

int blockSize = BinaryPrimitives.ReverseEndianness(info.BlockSize);

return new(width, height, blockSize);
}

}

}