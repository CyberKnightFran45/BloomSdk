using System;

namespace NetworkMgr.Cryptor.Raw
{
/// <summary> Helper used for Encrypting Packets from different Versions </summary>

public static class CryptoHelper
{
/** <summary> Encrypts or Decrypts the providen Http Data. </summary>

<param name="data"> Data to cipher </param>
<param name="type"> The Request/Response Type </param>
<param name="version"> Packet Version </param>
<param name="forEncryption"> Encryption mode </param>

<returns> A string containing the data ciphered </returns> */

private static string CipherData(ReadOnlySpan<char> data, ReadOnlySpan<char> type, PacketVersion version,
bool forEncryption)
{

return version switch
{
PacketVersion.V2 => PacketCryptorV2.CipherData(data, type, forEncryption),
_ => PacketCryptor.CipherData(data, type, forEncryption)
};

}

// Create delegate

public static PacketCipher CreateCipher(PacketVersion version, bool forEncryption)
{
return (data, type) => CipherData(data, type, version, forEncryption);
}

}

}