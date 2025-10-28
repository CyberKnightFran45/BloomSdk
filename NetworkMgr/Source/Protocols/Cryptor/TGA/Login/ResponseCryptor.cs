using System;
using System.IO;
using SexyCryptor;

namespace NetworkMgr.Cryptor.TGA.Login
{
/// <summary> Initializes Ciphering Tasks for Login Response. </summary>

public static class ResponseCryptor
{
// Encrypts a Login Response

public static void Encrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("Login Started: Encrypt Response");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading response body...");
var response = JsonSerializer.DeserializeObject<LoginResponseSchema>(inFile, LoginResponseSchema.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encrypting data...");

var crypto = LoginResponseEncryptedSchema.FromPlain(response);
var cryptoJson = JsonSerializer.SerializeObject(crypto, XResponseEncryptedSchema.Context);

string rawResponse = TWSecurity.CipherData(cryptoJson, true);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encrypted response...");
outFile.WriteString(rawResponse);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt response");
}

TraceLogger.Write("[SERVER] Login Encryption Finished");
}

// Decrypts a Response File

public static void Decrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("Login Started: Decrypt Response");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading response body...");

using var rOwner = inFile.ReadString();
var inputStr = rOwner.AsSpan();

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data...");

string rawResponse = TWSecurity.CipherData(inputStr, false);
var context = XResponseEncryptedSchema.Context;

var response = JsonSerializer.DeserializeObject<LoginResponseEncryptedSchema>(rawResponse, context);
var plain = LoginResponseSchema.FromEncrypted(response);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving decrypted response...");
JsonSerializer.SerializeObject(plain, outFile, LoginResponseSchema.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt response");
}

TraceLogger.Write("[SERVER] Login Decryption Finished");
}

}

}