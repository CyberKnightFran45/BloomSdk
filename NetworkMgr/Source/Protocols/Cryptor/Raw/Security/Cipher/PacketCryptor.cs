using System;

namespace NetworkMgr.Cryptor.Raw
{
/// <summary> Ciphers the Data contained in Http Packets from <c>e</c> node </summary>

internal static class PacketCryptor
{
/** <summary> Encrypts or Decrypts the providen Http Data by using Rijndael and Base64. </summary>

<param name="data"> Data to cipher (for Encryption: a Json string; for Decryption: Base64 string) </param>
<param name="vType"> The Request/Response Type </param>
<param name="forEncryption"> Encryption mode </param>

<returns> A string containing the data ciphered </returns> */

public static string CipherData(ReadOnlySpan<char> data, ReadOnlySpan<char> vType, bool forEncryption)
{
var key = KeyFactory.GetKey(vType);
var iv = KeyFactory.GetIV(vType, key);

return JsonCryptor.CipherData(data, key, iv, forEncryption);
}

}

}