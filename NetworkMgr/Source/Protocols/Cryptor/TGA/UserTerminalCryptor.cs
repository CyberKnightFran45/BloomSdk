using System;
using System.IO;

namespace NetworkMgr.Cryptor.TGA
{
/// <summary> Initializes Ciphering Tasks for UserTerminal. </summary>

public static class UserTerminalCryptor
{
// Encrypts a Login Request

public static void Encrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("User Terminal: Encryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading terminal info...");
var info = JsonSerializer.DeserializeObject<UserTerminalSchema>(inFile, UserTerminalSchema.Context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encrypting data...");
var crypto = UserTerminalEncryptedSchema.FromPlain(info);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encrypted form...");
crypto.WriteForm(outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt info");
}

TraceLogger.Write("UserTerminal Encrypt Finished");
}

// Decrypts a Request File

public static void Decrypt(string inputPath, string outputPath)
{
TraceLogger.Init();

TraceLogger.WriteLine("User Terminal: Decryption Started");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading form...");

UserTerminalEncryptedSchema info = new();
info.ReadForm(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data...");
var plain = UserTerminalSchema.FromEncrypted(info);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving decrypted info...");
JsonSerializer.SerializeObject(plain, outFile, UserTerminalSchema.Context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt info");
}

TraceLogger.Write("UserTerminal Decrypt Finished");
}

}

}