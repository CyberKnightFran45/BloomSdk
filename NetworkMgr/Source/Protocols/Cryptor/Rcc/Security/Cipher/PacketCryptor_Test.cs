using System;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Variant of the PacketCryptor (Test Version). </summary>

internal static class PacketCryptor_Test
{
// Default version

private const string VERSION = "none";

/** <summary> Encrypts or Decrypts the providen Http Data by using Rijndael and Base64. </summary>

<param name="data"> Data to cipher (for Encryption: a Json string; for Decryption: Base64 string) </param>
<param name="version"> The Request/Response Version </param>
<param name="forEncryption"> Encryption mode </param>

<returns> A string containing the data ciphered </returns> */

public static string CipherData(ReadOnlySpan<char> data, bool forEncryption)
{
var key = KeyFactory.GetKey(VERSION);
var iv = KeyFactory.GetIV(VERSION, key);

return JsonCryptor.CipherData(data, key, iv, forEncryption);
}

}

}