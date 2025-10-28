using System;
using System.IO;
using BlossomLib.Modules.Security;

namespace NetworkMgr
{
/// <summary> Initializes Ciphering Funcs for TW Cache </summary>

public static class TwCache
{
/// <summary> The Key used. </summary>

private static readonly byte[] KEY = "1234567890aba45678901234"u8.ToArray();

// Encrypt Json String

private static string EncryptJson(ReadOnlySpan<char> data)
{
var rawBytes = InputHelper.GetBytes(data);
using var xOwner = X3Des.Encrypt(rawBytes, KEY);

return new(xOwner.AsSpan() );
}

// Encrypt Model

public static TwCacheModel Encrypt(TwCacheModel plain)
{
string json = JsonSerializer.SerializeObject(plain, TwCacheModel.Context);
string cryptoJson = EncryptJson(json);

return new(cryptoJson);
}

/** <summary> Encrypts a TW CacheModel by using 3-DES and Hex </summary>

<param name = "inputPath"> The Path where the File to Encode is Located. </param>
<param name = "outputPath"> The Location where the Encoded File will be Saved. </param> */

public static void EncryptFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TWCache Encryption Started");

try
{
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading cache...");
var cache = JsonSerializer.DeserializeObject<TwCacheModel>(inFile, TwCacheModel.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encrypting data..");
var encryptedCache = Encrypt(cache);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving cache...");
JsonSerializer.SerializeObject(outFile, TwCacheModel.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt file");
}

TraceLogger.WriteLine("TWCache Encryption Finished");
}

// Decrypt Hex Data as Json

private static string DecryptJson(ReadOnlySpan<char> data)
{
var rawData = X3Des.Decrypt(data, KEY);

return InputHelper.GetString(rawData);
}

// Decrypt Model

public static TwCacheModel Decrypt(TwCacheModel crypto)
{
string json = DecryptJson(crypto.Data);
var cache = JsonSerializer.DeserializeObject<TwCacheModel>(json, TwCacheModel.Context);

return new(cache.Data, cache.Key);
}

/** <summary> Decrypts a TW CacheModel by using 3-DES and Hex </summary>

<param name = "inputPath"> The Path where the File to Encode is Located. </param>
<param name = "outputPath"> The Location where the Encoded File will be Saved. </param> */

public static void DecryptFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TWCache Decryption Started");

try
{
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading cache...");
var encryptedCache = JsonSerializer.DeserializeObject<TwCacheModel>(inFile, TwCacheModel.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data..");
var cache = Decrypt(encryptedCache);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving cache...");
JsonSerializer.SerializeObject(outFile, TwCacheModel.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt file");
}

TraceLogger.WriteLine("TWCache Decryption Finished");
}

}

}