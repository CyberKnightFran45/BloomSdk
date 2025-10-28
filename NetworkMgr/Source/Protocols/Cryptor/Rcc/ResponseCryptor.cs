namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Initializes Ciphering Tasks for RCC Response. </summary>

public static class ResponseCryptor
{
// Encrypts a Response File

public static void Encrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("RechargeCheck Started: Encrypt Response");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var encryptor = CryptoHelper.CreateCipher(version, true);
IResponseCryptor.Encrypt(inputPath, outputPath, encryptor, false);

TraceLogger.Write("[SERVER] RCC Encryption Finished");
}

// Decrypts a Response File

public static void Decrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("RechargeCheck Started: Decrypt Response");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var decryptor = CryptoHelper.CreateCipher(version, false);
IResponseCryptor.Decrypt(inputPath, outputPath, decryptor, false);

TraceLogger.Write("[SERVER] RCC Decryption Finished");
}

}

}