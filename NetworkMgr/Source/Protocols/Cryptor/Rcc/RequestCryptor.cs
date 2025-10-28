namespace NetworkMgr.Cryptor.Rcc
{
/// <summary> Initializes Ciphering Tasks for RCC Request. </summary>

public static class RequestCryptor
{
// Encrypts a Request File

public static void Encrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("RechargeCheck Started: Encrypt Request");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var encryptor = CryptoHelper.CreateCipher(version, true);
IRequestCryptor.Encrypt(inputPath, outputPath, encryptor, false);

TraceLogger.Write("[CLIENT] RCC Encryption Finished");
}

// Decrypts a Request File

public static void Decrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("RechargeCheck Started: Decrypt Request");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var decryptor = CryptoHelper.CreateCipher(version, false);
IRequestCryptor.Decrypt(inputPath, outputPath, decryptor, false);

TraceLogger.Write("[CLIENT] RCC Decryption Finished");
}

}

}