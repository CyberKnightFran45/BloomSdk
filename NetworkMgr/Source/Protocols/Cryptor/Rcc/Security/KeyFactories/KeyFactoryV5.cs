using RawGenerator = NetworkMgr.Cryptor.Raw.KeyGenerator;

using System;
using System.Collections.Generic;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Variant of the Key Factory, used in the V5.0 of the Algorithm </summary>

internal static class KeyFactoryV5
{
// Store IVs in Cache

private static readonly Dictionary<string, byte[]> _cachedIVs = new();

// Get Key from Cache or Make new one

public static byte[] GetKey(ReadOnlySpan<char> vType) => KeyGenerator.GetKey(vType);

// Get IV from Cache or Init a new one

public static byte[] GetIV(ReadOnlySpan<char> vType, ReadOnlySpan<byte> key)
{
string ivFlags = vType.ToString();

if(_cachedIVs.TryGetValue(ivFlags, out var iv) )
return iv;

var newIV = RawGenerator.InitV(vType, key);
_cachedIVs[ivFlags] = newIV;

return newIV;
}

}

}