namespace NetworkMgr.Cryptor.Raw
{
/// <summary> Initializes Ciphering Tasks for Raw Request. </summary>

public static class RequestCryptor
{
// Encrypts a Request File

public static void Encrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("Request Encryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var encryptor = CryptoHelper.CreateCipher(version, true);
IRequestCryptor.Encrypt(inputPath, outputPath, encryptor);

TraceLogger.Write("[CLIENT] Data Encryption Finished");
}

// Decrypts a Request File

public static void Decrypt(string inputPath, string outputPath, PacketVersion version)
{
TraceLogger.Init();

TraceLogger.WriteLine("Request Decryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} ({version})");

var decryptor = CryptoHelper.CreateCipher(version, false);
IRequestCryptor.Decrypt(inputPath, outputPath, decryptor);

TraceLogger.Write("[CLIENT] Data Decryption Finished");
}

}

}