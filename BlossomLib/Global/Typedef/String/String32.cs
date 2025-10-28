using System;
using System.Buffers.Binary;
using System.Text;

// Convert between uint and String

public static class String32
{
private static readonly Encoding encoding = EncodeHelper.GetEncoding(EncodingType.ISO_8859_1);

// Conver string to uint

public static uint ToInt(ReadOnlySpan<char> v)
{

if(v.IsEmpty)
return 0u;

if(v.Length > 8)
v = v[..8];

int strLen = encoding.GetByteCount(v);
Span<byte> buffer = stackalloc byte[strLen];

encoding.GetBytes(v, buffer);

return BinaryPrimitives.ReadUInt32BigEndian(buffer);
}

// Convert uint to string

public static string FromInt(uint v)
{

if(v == 0)
return null;

Span<byte> buffer = stackalloc byte[4];
BinaryPrimitives.WriteUInt32BigEndian(buffer, v);

return encoding.GetString(buffer);
}

}