using System;
using System.IO;

namespace NetworkMgr.Cryptor
{
/// <summary> Initializes Ciphering Tasks for Generic Responses. </summary>

public static class IResponseCryptor
{
// Encrypts a Response File

public static void Encrypt(string inputPath, string outputPath, PacketCipher encryptor,
bool importMap = true)
{

try
{
TraceLogger.WriteActionStart("Loading response body...");
var response = ServerResponseSchema.Read(inputPath);

TraceLogger.WriteActionEnd();

JsonPacketMap jsonMap = null;

if(importMap)
{
var pathToJMap = Path.Combine(Path.GetDirectoryName(inputPath), $"RespInfo{response.ResponseType}.json");

if(File.Exists(pathToJMap) || !FileManager.FileIsEmpty(pathToJMap) )
{
TraceLogger.WriteActionStart("Loading JsonMap...");

using var mapFile = FileManager.OpenRead(pathToJMap);
jsonMap = JsonSerializer.DeserializeObject<JsonPacketMap>(mapFile, JsonPacketMap.Context);

TraceLogger.WriteActionEnd();
}

}

TraceLogger.WriteActionStart("Encrypting data...");
var crypto = ServerResponseEncryptedSchema.FromPlain(response, encryptor, jsonMap);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encrypted response...");

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(crypto, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt response");
}

}

// Decrypts a Request File

public static void Decrypt(string inputPath, string outputPath, PacketCipher decryptor,
bool exportMap = true)
{

try
{
TraceLogger.WriteActionStart("Loading response body...");
using var inFile = FileManager.OpenRead(inputPath);

var inContext = ServerResponseEncryptedSchema.Context;
var response = JsonSerializer.DeserializeObject<ServerResponseEncryptedSchema>(inFile, inContext);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decrypting data...");

JsonPacketMap jsonMap = null;
ServerResponseSchema plain;

if(exportMap)
plain = ServerResponseSchema.FromEncrypted(response, decryptor, out jsonMap);

else
plain = ServerResponseSchema.FromEncrypted(response, decryptor, out _);

TraceLogger.WriteActionEnd();

if(jsonMap is not null)
{
var pathToJMap = Path.Combine(Path.GetDirectoryName(outputPath), $"RespInfo{plain.ResponseType}.json");
PathHelper.CheckDuplicatedPath(ref pathToJMap);

TraceLogger.WriteActionStart("Saving JsonMap...");

using var mapFile = FileManager.OpenWrite(pathToJMap);
JsonSerializer.SerializeObject(jsonMap, mapFile, JsonPacketMap.Context);

TraceLogger.WriteActionEnd();
}

TraceLogger.WriteActionStart("Saving decrypted response...");

using var outFile = FileManager.OpenWrite(outputPath);
JsonSerializer.SerializeObject(plain, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt response");
}

}

}

}