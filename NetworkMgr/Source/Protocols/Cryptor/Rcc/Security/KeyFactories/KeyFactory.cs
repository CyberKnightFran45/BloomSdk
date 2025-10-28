using System;
using System.Collections.Generic;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Serves as a Temporary Storage for Keys and IVs,
/// saving them in cache for fast Access </summary>

internal static class KeyFactory
{
// Store IVs in Cache

private static readonly Dictionary<string, byte[]> _cachedIVs = new();

// Get Key from Cache or Make new one

public static byte[] GetKey(ReadOnlySpan<char> version) => KeyGenerator.GetKey(version);

// Get Hash Index

private static int GetHashIndex(ReadOnlySpan<char> version)
{
bool isEmpty = version.IsEmpty || version.SequenceEqual("none");

return isEmpty ? 0 : InputHelper.FilterNumber<int>(version) * 2 + 1;
}

// Init Vector

private static byte[] InitV(ReadOnlySpan<char> version, ReadOnlySpan<byte> key)
{
int hashIndex = GetHashIndex(version);

return CryptoParams.InitVector(key, 24, hashIndex);
}

// Get IV from Cache or Init a new one

public static byte[] GetIV(ReadOnlySpan<char> version, ReadOnlySpan<byte> key)
{
string ivFlags = version.ToString();

if(_cachedIVs.TryGetValue(ivFlags, out var iv) )
return iv;

var newIV = InitV(version, key);
_cachedIVs[ivFlags] = newIV;

return newIV;
}

}

}