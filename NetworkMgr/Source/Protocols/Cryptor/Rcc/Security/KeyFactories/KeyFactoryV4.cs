using System;
using System.Collections.Generic;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Variant of the Key Factory, used in the V4.0 of the Algorithm </summary>

internal static class KeyFactoryV4
{
// Store IVs in Cache

private static readonly Dictionary<string, byte[]> _cachedIVs = new();

// Get Key from Cache or Make new one

public static byte[] GetKey(ReadOnlySpan<char> vType) => KeyGenerator.GetKey(vType);

// Get Hash Index

private static int GetHashIndex(ReadOnlySpan<char> version)
{
var versionNum = InputHelper.FilterNumber<int>(version);

return versionNum == 0 ? 3 : 2;
}

// Init Vector

private static byte[] InitV(ReadOnlySpan<char> version, ReadOnlySpan<byte> key)
{
int hashIndex = GetHashIndex(version);

return CryptoParams.InitVector(key, 24, hashIndex);
}

// Get IV from Cache or Init a new one

public static byte[] GetIV(ReadOnlySpan<char> vType, ReadOnlySpan<byte> key)
{
string ivFlags = vType.ToString();

if(_cachedIVs.TryGetValue(ivFlags, out var iv) )
return iv;

var newIV = InitV(vType, key);
_cachedIVs[ivFlags] = newIV;

return newIV;
}

}

}