using System;
using System.Collections.Generic;

namespace NetworkMgr.Cryptor.Raw
{
/// <summary> Serves as a Temporary Storage for Keys and IVs,
/// saving them in cache for fast Access </summary>

internal static class KeyFactory
{
// Key Prefix 

private static readonly byte[] KEY_PREFIX = "`jou*"u8.ToArray();

// Key Suffix

private static readonly byte[] KEY_SUFFIX = ")xoj'"u8.ToArray();

// Store Keys in Cache

private static readonly Dictionary<string, byte[]> _cachedKeys = new();

// Store IVs in Cache

private static readonly Dictionary<string, byte[]> _cachedIVs = new();

// Get Key from Cache or Make new one

public static byte[] GetKey(ReadOnlySpan<char> vType)
{
string keyFlags = vType.ToString();

if(_cachedKeys.TryGetValue(keyFlags, out var key) )
return key;

var newKey = KeyGenerator.MakeKey(vType, KEY_PREFIX, KEY_SUFFIX);
_cachedKeys[keyFlags] = newKey;

return newKey;
}

// Get IV from Cache or Init a new one

public static byte[] GetIV(ReadOnlySpan<char> vType, ReadOnlySpan<byte> key)
{
string ivFlags = vType.ToString();

if(_cachedIVs.TryGetValue(ivFlags, out var iv) )
return iv;

var newIV = KeyGenerator.InitV(vType, key);
_cachedIVs[ivFlags] = newIV;

return newIV;
}

}

}