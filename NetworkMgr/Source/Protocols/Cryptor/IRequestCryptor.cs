using System;
using System.IO;

namespace NetworkMgr.Cryptor
{
/// <summary> Initializes Ciphering Tasks for Generic Requests. </summary>

public static class IRequestCryptor
{
// Encrypts a Request File

public static void Encrypt(string inputPath, string outputPath, PacketCipher encryptor,
bool importMap = true)
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading request body...");
var request = ClientRequestSchema.Read(inputPath);

TraceLogger.WriteActionEnd();

JsonPacketMap jsonMap = null;

if(importMap)
{
var pathToJMap = Path.Combine(Path.GetDirectoryName(inputPath), $"ReqInfo{request.RequestType}.json");

if(File.Exists(pathToJMap) || !FileManager.FileIsEmpty(pathToJMap) )
{
TraceLogger.WriteActionStart("Loading JsonMap...");

using var mapFile = FileManager.OpenRead(pathToJMap);
jsonMap = JsonSerializer.DeserializeObject<JsonPacketMap>(mapFile, JsonPacketMap.Context);

TraceLogger.WriteActionEnd();
}

}

TraceLogger.WriteActionStart("Encrypting data...");
var crypto = ClientRequestEncryptedSchema.FromPlain(request, encryptor, jsonMap);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encrypted form...");
crypto.WriteForm(outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt request");
}

}

// Decrypts a Request File

public static void Decrypt(string inputPath, string outputPath, PacketCipher decryptor,
bool exportMap = true)
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading request form...");

ClientRequestEncryptedSchema request = new();
request.ReadForm(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data...");

JsonPacketMap jsonMap = null;
ClientRequestSchema plain;

if(exportMap)
plain = ClientRequestSchema.FromEncrypted(request, decryptor, out jsonMap);

else
plain = ClientRequestSchema.FromEncrypted(request, decryptor, out _);

TraceLogger.WriteActionEnd();

if(jsonMap is not null)
{
string pathToJMap = Path.Combine(Path.GetDirectoryName(outputPath), $"ReqInfo{plain.RequestType}.json");
PathHelper.CheckDuplicatedPath(ref pathToJMap);

TraceLogger.WriteActionStart("Saving JsonMap...");

using var mapFile = FileManager.OpenWrite(pathToJMap);
JsonSerializer.SerializeObject(jsonMap, mapFile, JsonPacketMap.Context);

TraceLogger.WriteActionEnd();
}

TraceLogger.WriteActionStart("Saving decrypted request...");
JsonSerializer.SerializeObject(plain, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt request");
}

}

}

}