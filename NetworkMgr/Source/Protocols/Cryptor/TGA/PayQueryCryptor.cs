using System;
using System.IO;
using SexyCryptor;

namespace NetworkMgr.Cryptor.TGA
{
/// <summary> Initializes Ciphering Tasks for PayQuery. </summary>

public static class PayQueryCryptor
{
// Encrypts a Login Response

public static void Encrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("PayQuery Started: Encrypt");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading PayQuery...");
var payQuery = JsonSerializer.DeserializeObject<PayQuerySchema>(inFile, PayQuerySchema.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encrypting data...");

var crypto = PayQueryEncryptedSchema.FromPlain(payQuery);
var cryptoJson = JsonSerializer.SerializeObject(crypto, XResponseEncryptedSchema.Context);

string rawResponse = TWSecurity.CipherData(cryptoJson, true);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encrypted data...");
outFile.WriteString(rawResponse);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt query");
}

TraceLogger.Write("PayQuery Encryption Finished");
}

// Decrypts a Response File

public static void Decrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("PayQuery Started: Decrypt");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading PayQuery...");

using var rOwner = inFile.ReadString();
var inputStr = rOwner.AsSpan();

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data...");

string rawResponse = TWSecurity.CipherData(inputStr, false);
var context =  PayQueryEncryptedSchema.Context;

var response = JsonSerializer.DeserializeObject<PayQueryEncryptedSchema>(rawResponse, context);
var plain = PayQuerySchema.FromEncrypted(response);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving decrypted data...");
JsonSerializer.SerializeObject(plain, outFile, PayQuerySchema.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt query");
}

TraceLogger.Write("PayQuery Decryption Finished");
}

}

}