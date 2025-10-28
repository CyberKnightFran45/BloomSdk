using System;
using System.IO;
using SexyCryptor;

namespace NetworkMgr.Cryptor.TGA.Login
{
/// <summary> Initializes Ciphering Tasks for PayRecord. </summary>

public static class PayRecordCryptor
{
// Encrypts a PayRecord

public static void Encrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("PayRecord: Encryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading record...");
var payRecord = JsonSerializer.DeserializeObject<PayRecordSchema>(inFile, PayRecordSchema.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encrypting data...");

var jsonStr = JsonSerializer.SerializeObject(payRecord, PayRecordSchema.Context);
string crypto = TWSecurity.CipherData(jsonStr, true);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encrypted data...");
outFile.WriteString(crypto);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt record");
}

TraceLogger.Write("PayRecord Encryption Finished");
}

// Decrypts a Response File

public static void Decrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("PayRecord: Decryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading record...");

using var rOwner = inFile.ReadString();
var inputStr = rOwner.AsSpan();

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data...");
string jsonStr = TWSecurity.CipherData(inputStr, false);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving decrypted data...");

var payRecord = JsonSerializer.DeserializeObject<PayRecordSchema>(jsonStr, PayRecordSchema.Context);
JsonSerializer.SerializeObject(payRecord, outFile, LoginResponseSchema.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt record");
}

TraceLogger.Write("PayRecord Decryption Finished");
}

}

}