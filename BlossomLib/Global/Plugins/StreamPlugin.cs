using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

public static class StreamPlugin
{
// READER METHODS

public static void ReadBytes(this Stream reader, Span<byte> buffer, Endianness endian)
{
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
buffer.Reverse();

}

public static NativeMemoryOwner<byte> ReadPtr(this Stream reader, Endianness endian = default)
{
return reader.ReadPtr(reader.Length, endian);
}

public static NativeMemoryOwner<byte> ReadPtr(this Stream reader, long count, Endianness endian = default)
{
const int CHUNK_SIZE = int.MaxValue;

var toRead = (ulong)count;
NativeMemoryOwner<byte> memOwner = new(toRead);

ulong totalRead = 0;

while(totalRead < toRead)
{
int blockSize = (int)Math.Min(CHUNK_SIZE, toRead - totalRead);

var buffer = memOwner.AsSpan(totalRead, blockSize);
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
buffer.Reverse();

totalRead += (ulong)blockSize;
}

return memOwner;
}

public static bool ReadBool(this Stream reader) => reader.ReadByte() != 0;

public static char ReadChar(this Stream reader, Endianness endian = default)
{
return (char)reader.ReadUInt16(endian);
}

public static byte ReadUInt8(this Stream reader)
{
int raw = reader.ReadByte();

if(raw == -1)
throw new Exception("Reach End of Stream");

return (byte)raw;
}

public static sbyte ReadInt8(this Stream reader) => (sbyte)reader.ReadUInt8();

public static short ReadInt16(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[2];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadInt16BigEndian(buffer);

return BinaryPrimitives.ReadInt16LittleEndian(buffer);
}

public static ushort ReadUInt16(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[2];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadUInt16BigEndian(buffer);

return BinaryPrimitives.ReadUInt16LittleEndian(buffer);
}

public static int ReadInt24(this Stream reader, Endianness endian = default)
{
uint v = reader.ReadUInt24(endian);

if( (v & 0x800000) != 0) 
v |= 0xFF000000;

return (int)v;
}

public static uint ReadUInt24(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[3];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return (uint)(buffer[2] | (buffer[1] << 8) | (buffer[0] << 16) );

return (uint)(buffer[0] | (buffer[1] << 8) | (buffer[2] << 16) );
}

public static int ReadInt32(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[4];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadInt32BigEndian(buffer);

return BinaryPrimitives.ReadInt32LittleEndian(buffer);
}

public static uint ReadUInt32(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[4];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadUInt32BigEndian(buffer);

return BinaryPrimitives.ReadUInt32LittleEndian(buffer);
}

public static long ReadInt64(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[8];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadInt64BigEndian(buffer);

return BinaryPrimitives.ReadInt64LittleEndian(buffer);
}

public static ulong ReadUInt64(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[8];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadUInt64BigEndian(buffer);

return BinaryPrimitives.ReadUInt64LittleEndian(buffer);
}

public static Int128 ReadInt128(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[16];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadInt128BigEndian(buffer);

return BinaryPrimitives.ReadInt128LittleEndian(buffer);
}

public static UInt128 ReadUInt128(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[16];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadUInt128BigEndian(buffer);

return BinaryPrimitives.ReadUInt128LittleEndian(buffer);
}

public static int ReadVarInt(this Stream reader)
{
int varInt = 0;
int iBase = 0;

byte input;

do
{

if(iBase == 35)
break;

input = reader.ReadUInt8();
varInt |= (input & 0x7F) << iBase;

iBase += 7;
}

while( (input & 0x80) != 0);

return varInt;
}

public static uint ReadVarUInt(this Stream reader) => (uint)reader.ReadVarInt();

public static long ReadVarInt64(this Stream reader)
{
long varInt = 0;
int iBase = 0;

byte input;

do
{

if(iBase == 70)
break;

input = reader.ReadUInt8();
varInt |= ( (long)(input & 0x7F) ) << iBase;

iBase += 7;
}

while( (input & 0x80) != 0);

return varInt;
}

public static ulong ReadVarUInt64(this Stream reader) => (ulong)reader.ReadVarInt64();

public static int ReadZigZag(this Stream reader)
{
var input = (uint)reader.ReadVarInt();

if( (input & 0b1) == 0)
return (int)(input >> 1);

return -(int)( (input + 1) >> 1);
}

public static long ReadZigZag64(this Stream reader)
{
var input = (ulong)reader.ReadVarInt64();

if( (input & 0b1) == 0)
return (long)(input >> 1);

return -(long)( (input + 1) >> 1);
}

public static float ReadFloat(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[4];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadSingleBigEndian(buffer);

return BinaryPrimitives.ReadSingleLittleEndian(buffer);
}

public static double ReadDouble(this Stream reader, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[8];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
return BinaryPrimitives.ReadDoubleBigEndian(buffer);

return BinaryPrimitives.ReadDoubleLittleEndian(buffer);
}

public static NativeString ReadString(this Stream reader, Encoding encoding = null,
Endianness endian = default)
{
return reader.ReadString(reader.Length, encoding, endian);
}

public static NativeString ReadString(this Stream reader, long strLen, Encoding encoding = null,
Endianness endian = default)
{

if(strLen <= SizeT.MAX_STACK)
{
Span<byte> buffer = stackalloc byte[(int)strLen];
reader.ReadExactly(buffer);

if(endian == Endianness.BigEndian)
buffer.Reverse();

return InputHelper.GetNativeString(buffer, encoding);
}

using var rawBytes = reader.ReadPtr(strLen, endian);

return InputHelper.GetNativeString(rawBytes.AsSpan(), encoding);
}

public static NativeString ReadStringByLen8(this Stream reader, Encoding encoding = null)
{
byte strLen = reader.ReadUInt8();

return reader.ReadString(strLen, encoding);
}

public static NativeString ReadStringByLen16(this Stream reader, Encoding encoding = null,
Endianness endian = default)
{
ushort strLen = reader.ReadUInt16(endian);

return reader.ReadString(strLen, encoding, endian);
}

public static NativeString ReadStringByLen32(this Stream reader, Encoding encoding = null,
Endianness endian = default)
{
uint strLen = reader.ReadUInt32(endian);

return reader.ReadString(strLen, encoding, endian);
}

public static NativeString ReadStringByLen64(this Stream reader, Encoding encoding = null,
Endianness endian = default)
{
long strLen = reader.ReadInt64(endian);

return reader.ReadString(strLen, encoding);
}

public static NativeString ReadStringByLen128(this Stream reader, Encoding encoding = null,
Endianness endian = default)
{
UInt128 bigLen = reader.ReadUInt128(endian);

long strLen = bigLen > (UInt128)long.MaxValue ? long.MaxValue 
: (long)(ulong)(bigLen & ulong.MaxValue);

return reader.ReadString(strLen, encoding, endian);
}

public static NativeString ReadStringByVarLen(this Stream reader, Encoding encoding = null)
{
long strLen = reader.ReadVarInt();

return reader.ReadString(strLen, encoding);
}

public static NativeString ReadStringByVarLen64(this Stream reader, Encoding encoding = null)
{
long strLen = reader.ReadVarInt64();

return reader.ReadString(strLen, encoding);
}

public static NativeString ReadCString(this Stream reader, Encoding encoding = null)
{
using NativeMemoryOwner<byte> buffer = new(64);
int length = 0;

while(true)
{
int raw = reader.ReadByte();

if(raw == -1)
break;

var b = (byte)raw;

if(b == 0x0)
break;

if(length >= (int)buffer.Size)
buffer.Realloc(buffer.Size * 2);

buffer.AsSpan()[length++] = b;	
}

return InputHelper.GetNativeString(buffer.AsSpan(0, length), encoding);
}

public static NativeString? ReadLine(this Stream reader, Encoding encoding = null)
{
using NativeMemoryOwner<byte> buffer = new(512);
int length = 0;

while(true)
{
int raw = reader.ReadByte();

if(raw == -1)
return null;

var b = (byte)raw;

if(b == 0x0D) // '\r'
{
int peek = reader.PeekByte();

if(peek == 0x0A) // Handle '\r\n' as '\n'
reader.ReadByte();

break;
}

else if(b == 0x0A) // '\n'
break;

if(length >= (int)buffer.Size)
buffer.Realloc(buffer.Size * 2);

buffer.AsSpan()[length++] = b;	
}

return InputHelper.GetNativeString(buffer.AsSpan(0, length), encoding);
}

// WRITER METHODS

public static void WriteBool(this Stream writer, bool v)
{
writer.WriteByte( (byte)(v ? 1u : 0u) );
}

public static void WriteChar(this Stream writer, char v, Endianness endian = default)
{
writer.WriteUInt16( (ushort)v, endian);
}

public static void WriteInt8(this Stream writer, sbyte v) => writer.WriteByte( (byte)v);

public static void WriteInt16(this Stream writer, short v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[2];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteInt16BigEndian(buffer, v);

else
BinaryPrimitives.WriteInt16LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteUInt16(this Stream writer, ushort v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[2];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteUInt16BigEndian(buffer, v);

else
BinaryPrimitives.WriteUInt16LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteInt24(this Stream writer, int v, Endianness endian = default)
{
writer.WriteUInt24( (uint)v, endian);
}

public static void WriteUInt24(this Stream writer, uint v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[3];

if(endian == Endianness.BigEndian)
{
buffer[2] = (byte)v;
buffer[1] = (byte)(v >> 8);
buffer[0] = (byte)(v >> 16);
}

else
{
buffer[0] = (byte)v;
buffer[1] = (byte)(v >> 8);
buffer[2] = (byte)(v >> 16);
}

writer.Write(buffer);
}

public static void WriteInt32(this Stream writer, int v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[4];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteInt32BigEndian(buffer, v);

else
BinaryPrimitives.WriteInt32LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteUInt32(this Stream writer, uint v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[4];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteUInt32BigEndian(buffer, v);

else
BinaryPrimitives.WriteUInt32LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteInt64(this Stream writer, long v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[8];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteInt64BigEndian(buffer, v);

else
BinaryPrimitives.WriteInt64LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteUInt64(this Stream writer, ulong v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[8];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteUInt64BigEndian(buffer, v);

else
BinaryPrimitives.WriteUInt64LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteInt128(this Stream writer, Int128 v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[16];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteInt128BigEndian(buffer, v);

else
BinaryPrimitives.WriteInt128LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteUInt128(this Stream writer, UInt128 v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[16];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteUInt128BigEndian(buffer, v);

else
BinaryPrimitives.WriteUInt128LittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteVarInt(this Stream writer, int v)
{

while(v > 0x7F)
{
writer.WriteByte( (byte)(v | 0x80) );
v >>= 7;
}

writer.WriteByte( (byte)v);
}

public static void WriteVarUInt(this Stream writer, uint v) => writer.WriteVarInt( (int)v);

public static void WriteVarInt64(this Stream writer, long v)
{

while(v > 0x7F)
{
writer.WriteByte( (byte)(v | 0x80) );
v >>= 7;
}

writer.WriteByte( (byte)v);
}

public static void WriteVarUInt64(this Stream writer, ulong v) => writer.WriteVarInt64( (long)v);

public static void WriteZigZag32(this Stream writer, int v)
{
int zigZag = (v << 1) ^ (v >> 31);

writer.WriteVarInt(zigZag);
}

public static void WriteZigZag64(this Stream writer, long v)
{
long zigZag = (v << 1) ^ (v >> 63);

writer.WriteVarInt64(zigZag);
}

public static void WriteFloat(this Stream writer, float v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[4];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteSingleBigEndian(buffer, v);

else
BinaryPrimitives.WriteSingleLittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteDouble(this Stream writer, double v, Endianness endian = default)
{
Span<byte> buffer = stackalloc byte[8];

if(endian == Endianness.BigEndian)
BinaryPrimitives.WriteDoubleBigEndian(buffer, v);

else
BinaryPrimitives.WriteDoubleLittleEndian(buffer, v);

writer.Write(buffer);
}

public static void WriteBytes(this Stream writer, Span<byte> bytes, Endianness endian = default)
{

if(endian == Endianness.BigEndian)
bytes.Reverse();

writer.Write(bytes);
}

public static void WriteString(this Stream writer, ReadOnlySpan<char> v, Encoding encoding = null,
Endianness endian = default)
{
using var buffer = InputHelper.GetNativeBytes(v, encoding);

writer.WriteBytes(buffer.AsSpan(), endian);
}

public static void WriteStringByLen8(this Stream writer, ReadOnlySpan<char> v, Encoding encoding = null)
{

if(v.IsEmpty)
{
writer.WriteByte(0);
return;
}

writer.WriteByte( (byte)v.Length);
writer.WriteString(v, encoding);
}

public static void WriteStringByLen16(this Stream writer, ReadOnlySpan<char> v,
Encoding encoding = null, Endianness endian = default)
{

if(v.IsEmpty)
{
writer.WriteUInt16(0);
return;
}

writer.WriteUInt16( (ushort)v.Length, endian);
writer.WriteString(v, encoding, endian);
}

public static void WriteStringByLen32(this Stream writer, ReadOnlySpan<char> v,
Encoding encoding = null, Endianness endian = default)
{

if(v.IsEmpty)
{
writer.WriteUInt32(0);
return;
}

writer.WriteUInt32( (uint)v.Length, endian);
writer.WriteString(v, encoding, endian);
}

public static void WriteStringByLen64(this Stream writer, ReadOnlySpan<char> v,
Encoding encoding = null, Endianness endian = default)
{

if(v.IsEmpty)
{
writer.WriteUInt64(0);
return;
}

writer.WriteUInt64( (ulong)v.Length, endian);
writer.WriteString(v, encoding, endian);
}

public static void WriteStringByLen128(this Stream writer, ReadOnlySpan<char> v,
Encoding encoding = null, Endianness endian = default)
{

if(v.IsEmpty)
{
writer.WriteUInt128(UInt128.Zero);
return;
}

var strLen = (ulong)v.Length;
UInt128 bigInt = new(strLen, strLen);

writer.WriteUInt128(bigInt, endian);
writer.WriteString(v, encoding, endian);
}

public static void WriteStringByVarLen(this Stream writer, ReadOnlySpan<char> v, Encoding encoding = null)
{

if(v.IsEmpty)
{
writer.WriteVarInt(0);
return;
}

writer.WriteVarInt(v.Length);
writer.WriteString(v, encoding);
}

public static void WriteStringByVarLen64(this Stream writer, ReadOnlySpan<char> v, Encoding encoding = null)
{

if(v.IsEmpty)
{
writer.WriteVarInt64(0);
return;
}

writer.WriteVarInt64(v.Length);
writer.WriteString(v, encoding);
}

public static void WriteCString(this Stream writer, ReadOnlySpan<char> v, Encoding encoding = null)
{
writer.WriteString(v, encoding);
writer.WriteChar('\0');
}

public static void WriteLine(this Stream writer, ReadOnlySpan<char> v, Encoding encoding = null)
{
writer.WriteString(v, encoding);
writer.WriteString(Environment.NewLine, encoding);
}

// OTHER

public static void Pad(this Stream writer, int alignment, byte padding = 0x0)
{

if(alignment <= 0)
return;

var required = SizeT.GetPadding(writer.Length, alignment);
using NativeMemoryOwner<byte> pOwner = new(required);

pOwner.Fill(padding);
writer.Write(pOwner.AsSpan() );
}

public static int PeekByte(this Stream stream)
{

if(!stream.CanSeek)
throw new NotSupportedException("PeekByte requires a seekable stream");

long pos = stream.Position;
int b = stream.ReadByte();

if(b != -1)
stream.Position = pos;

return b;
}


}