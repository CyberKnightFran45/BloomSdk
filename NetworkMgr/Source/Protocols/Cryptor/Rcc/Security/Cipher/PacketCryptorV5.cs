using System;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Variation of the PacketCryptor, used in last Versions </summary>

internal static class PacketCryptorV5
{
/** <summary> Encrypts or Decrypts the providen Http Data by using Rijndael and Base64. </summary>

<param name="data"> Data to cipher (for Encryption: a Json string; for Decryption: Base64 string) </param>
<param name="vType"> The Request/Response Type </param>
<param name="forEncryption"> Encryption mode </param>

<returns> A string containing the data ciphered </returns> */

public static string CipherData(ReadOnlySpan<char> data, ReadOnlySpan<char> vType, bool forEncryption)
{
var key = KeyFactoryV5.GetKey(vType);
var iv = KeyFactoryV5.GetIV(vType, key);

return JsonCryptor.CipherData(data, key, iv, forEncryption);
}

}

}