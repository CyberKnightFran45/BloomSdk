using System;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Ciphers the Data contained in Http Packets from <c>e</c> node </summary>

internal static class PacketCryptor
{
/** <summary> Encrypts or Decrypts the providen Http Data by using Rijndael and Base64. </summary>

<param name="data"> Data to cipher (for Encryption: a Json string; for Decryption: Base64 string) </param>
<param name="version"> The Request/Response Version </param>
<param name="forEncryption"> Encryption mode </param>

<returns> A string containing the data ciphered </returns> */

public static string CipherData(ReadOnlySpan<char> data, ReadOnlySpan<char> version, bool forEncryption)
{
var key = KeyFactory.GetKey(version);
var iv = KeyFactory.GetIV(version, key);

return JsonCryptor.CipherData(data, key, iv, forEncryption);
}

}

}