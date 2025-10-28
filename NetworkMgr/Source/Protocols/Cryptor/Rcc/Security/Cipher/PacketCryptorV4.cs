using System;

namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> PacketCryptor, for Version 4.0 of Algorithm </summary>

internal static class PacketCryptorV4
{
/** <summary> Encrypts or Decrypts the providen Http Data by using Rijndael and Base64. </summary>

<param name="data"> Data to cipher (for Encryption: a Json string; for Decryption: Base64 string) </param>
<param name="vType"> The Request/Response Type </param>
<param name="forEncryption"> Encryption mode </param>

<returns> A string containing the data ciphered </returns> */

public static string CipherData(ReadOnlySpan<char> data, ReadOnlySpan<char> vType, bool forEncryption)
{
var key = KeyFactoryV4.GetKey(vType);
var iv = KeyFactoryV4.GetIV(vType, key);

return JsonCryptor.CipherData(data, key, iv, forEncryption);
}

}

}