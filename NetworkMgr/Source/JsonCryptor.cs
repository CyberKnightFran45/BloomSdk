using BlossomLib.Modules.Parsers;
using BlossomLib.Modules.Security;
using System;

namespace NetworkMgr
{
/// <summary> Encrypts or Decrypts JsonData with Rijndael + Base64 (Web-safe) </summary>

public static class JsonCryptor
{
/// <summary> The Block Size used. </summary>

private const RijndaelBlockSize BLOCK_SIZE = RijndaelBlockSize.SIZE_24;

// Check Encoded String

public static bool IsValid64(ReadOnlySpan<char> str, out NativeString output)
{
output = new();

if(str.Length % 32 != 0)
return false;

bool isBase64 = Base64.IsWebSafe(str, out var rOwner);

if(!isBase64)
return false;

output = InputHelper.GetNativeString(rOwner.AsSpan() );

return true;
}

// Encrypts a Json String, then Encodes it

private static string Encrypt(ReadOnlySpan<char> data, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
{
using var rOwner = InputHelper.GetNativeBytes(data);
using var encOwner = Rijndael64.Encrypt(rOwner.AsSpan(), key, iv, BLOCK_SIZE);

return encOwner.ToString();
}

// Decodes a Base64 String, then Decrypts the Json data

private static string Decrypt(ReadOnlySpan<char> data, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv)
{
using var decOwner = Rijndael64.Decrypt(data, key, iv, BLOCK_SIZE);

return InputHelper.GetString(decOwner.AsSpan() );
}

/** <summary> Ciphers the providen Data with Rijndael 192-bits (CBC Mode), then parse it with Base64. </summary>

<param name="data"> Data to cipher (represented as a String) </param>
<param name="key"> The Key </param>
<param name="iv"> Initialization Vector </param>
<param name="forEncryption"> Encryption mode </param>

<returns> A string containing the data ciphered </returns> */

public static string CipherData(ReadOnlySpan<char> data, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv,
bool forEncryption)
{
return forEncryption ? Encrypt(data, key, iv) : Decrypt(data, key, iv);
}

}

}