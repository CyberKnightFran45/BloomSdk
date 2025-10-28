using System;
using System.Text.Json.Serialization;

namespace NetworkMgr.Parser
{
/// <summary> Encodes TGALogs and HTTP Documents between JSON and their respective formats. </summary>

public static class TGAParser
{
// Convert Log from JSON to TGA

public static void Encode<T>(string inputPath, string outputPath, JsonSerializerContext context = null)
where T : ILoggable<T>
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using var inFile = FileManager.OpenRead(inputPath);
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading JSON...");
var rawJson = JsonSerializer.DeserializeObject<T>(inFile, context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Encoding data...");
var parsedLog = TGALog.FromRaw(rawJson);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving encoded form...");
parsedLog.WriteForm(outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode Log");
}

}

// Convert Log from JSON to HTTP

public static void EncodeHttp<T>(string inputPath, string outputPath, JsonSerializerContext context = null)
where T : HttpDoc<T>
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using var inFile = FileManager.OpenRead(inputPath);
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading JSON...");
T doc = JsonSerializer.DeserializeObject<T>(inFile, context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving HTTP Form...");
doc.WriteForm(outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode HTTP Document");
}

}

// Parse Json as HTTP GET

public static void EncodeHttpGet<T>(string baseUrl, string inputPath, string outputPath,
JsonSerializerContext context = null) where T : HttpUrlDoc<T>
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using var inFile = FileManager.OpenRead(inputPath);
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading JSON...");
var order = JsonSerializer.DeserializeObject<T>(inFile, context);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart($"Building Query for {baseUrl}...");
string query = UrlFetcher.BuildQuery(baseUrl, order);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Writting GET Request...");
UrlFetcher.BuildGetRequest(query, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode Request");
}

}

// Convert TAG Log to JSON

public static void Decode<T>(string inputPath, string outputPath, JsonSerializerContext context = null)
where T : ILoggable<T>, new()
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using var inFile = FileManager.OpenRead(inputPath);
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading form...");

TGALog parsedLog = new();
parsedLog.ReadForm(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Decoding data...");

T plainJson = new();
plainJson.Deserialize(parsedLog);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving JSON...");
JsonSerializer.SerializeObject(plainJson, outFile, context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode Log");
}

}

// Convert Log from HTTP to JSON

public static void DecodeHttp<T>(string inputPath, string outputPath, JsonSerializerContext context = null)
where T : HttpDoc<T>, new()
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using var inFile = FileManager.OpenRead(inputPath);
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Loading HTTP Form...");

T doc = new();
doc.ReadForm(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving JSON...");
JsonSerializer.SerializeObject(doc, outFile, context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode HTTP Document");
}

}

// Convert HTTP GET to JSON

public static void DecodeHttpGet<T>(string inputPath, string outputPath, JsonSerializerContext context = null)
where T : HttpUrlDoc<T>, new()
{

try
{
TraceLogger.WriteActionStart("Opening files...");

using var inFile = FileManager.OpenRead(inputPath);
using var outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Reading GET Request...");
string query = UrlFetcher.ParseGetRequest(inFile);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Parsing query...");
var order = UrlFetcher.ParseQuery<T>(query);

TraceLogger.WriteActionEnd();

TraceLogger.WriteActionStart("Saving JSON...");
JsonSerializer.SerializeObject(order, outFile, context);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode Request");
}

}

}

}