using BlossomLib.Modules.Security;
using System;

namespace NetworkMgr.Cryptor.Raw
{
/// <summary> Generates Keys by using a value with a Prefix and Suffix </summary>

internal static class KeyGenerator
{
/** <summary> Makes a unique Key by Hashing the Request/Response Type. </summary>

<remarks> vType is <c>i</c> node from Request, or <c>req</c> node from Response. </remarks> */

public static byte[] MakeKey(ReadOnlySpan<char> vType, ReadOnlySpan<byte> prefix, ReadOnlySpan<byte> suffix)
{
using var vOwner = InputHelper.GetNativeBytes(vType);
var vBytes = vOwner.AsSpan();

int rawLen = vBytes.Length + prefix.Length + suffix.Length;

using NativeMemoryOwner<byte> rOwner = new(rawLen);
Span<byte> rawKey = rOwner.AsSpan();

prefix.CopyTo(rawKey);
vBytes.CopyTo(rawKey[prefix.Length..]);
suffix.CopyTo(rawKey[(prefix.Length + vBytes.Length)..] );

using var mOwner = GenericDigest.GetString(rawKey, "MD5");
using var hOwner = InputHelper.GetNativeBytes(mOwner.AsSpan() );

return hOwner.ToArray();
}

// Get Hash Index

private static int GetHashIndex(ReadOnlySpan<char> vType)
{
return vType.IsEmpty ? 0 : InputHelper.FilterNumber<int>(vType) % 7;
}

// Init Vector

public static byte[] InitV(ReadOnlySpan<char> vType, ReadOnlySpan<byte> key)
{
int hashIndex = GetHashIndex(vType);

return CryptoParams.InitVector(key, 24, hashIndex);
}

}

}