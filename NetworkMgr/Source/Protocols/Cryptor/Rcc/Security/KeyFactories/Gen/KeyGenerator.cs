using RawGenerator = NetworkMgr.Cryptor.Raw.KeyGenerator;

using System;
using System.Collections.Generic;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Generates Keys by using a value with a Prefix </summary>

internal static class KeyGenerator
{
// Key Prefix 

private static readonly byte[] KEY_PREFIX = "ila&master"u8.ToArray();

// Store Keys in Cache

private static readonly Dictionary<string, byte[]> _cachedKeys = new();

// Make Key

private static byte[] MakeKey(ReadOnlySpan<char> version) => RawGenerator.MakeKey(version, KEY_PREFIX, []);

// Get Key from Cache or Make new one

public static byte[] GetKey(ReadOnlySpan<char> version)
{
string keyFlags = version.ToString();

if(_cachedKeys.TryGetValue(keyFlags, out var key) )
return key;

var newKey = MakeKey(version);
_cachedKeys[keyFlags] = newKey;

return newKey;
}

}

}