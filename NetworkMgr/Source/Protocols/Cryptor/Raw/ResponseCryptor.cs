namespace NetworkMgr.Cryptor.Raw
{
/// <summary> Initializes Ciphering Tasks for Raw Response. </summary>

public static class ResponseCryptor
{
// Encrypts a Response File

public static void Encrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("Response Encryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var encryptor = CryptoHelper.CreateCipher(version, true);
IResponseCryptor.Encrypt(inputPath, outputPath, encryptor);

TraceLogger.Write("[SERVER] Data Encryption Finished");
}

// Decrypts a Response File

public static void Decrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("Response Decryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var decryptor = CryptoHelper.CreateCipher(version, false);
IResponseCryptor.Decrypt(inputPath, outputPath, decryptor);

TraceLogger.Write("[SERVER] Data Decryption Finished");
}

}

}