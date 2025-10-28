using System;
using System.IO;

namespace NetworkMgr.Cryptor.TGA.Login
{
/// <summary> Initializes Ciphering Tasks for Login Request. </summary>

public static class RequestCryptor
{
// Encrypts a Login Request

public static void Encrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("Login Started: Encrypt Request");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading request body...");
var request = JsonSerializer.DeserializeObject<LoginRequestSchema>(inFile, LoginRequestSchema.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encrypting data...");
var crypto = LoginRequestEncryptedSchema.FromPlain(request);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encrypted form...");
crypto.WriteForm(outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt request");
}

TraceLogger.Write("[CLIENT] Login Encryption Finished");
}

// Decrypts a Request File

public static void Decrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("Login Started: Decrypt Request");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading request form...");

LoginRequestEncryptedSchema request = new();
request.ReadForm(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data...");
var plain = LoginRequestSchema.FromEncrypted(request);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving decrypted request...");
JsonSerializer.SerializeObject(plain, outFile, LoginRequestSchema.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt request");
}

TraceLogger.Write("[CLIENT] Login Decryption Finished");
}

}

}