using System;
using System.IO;
using System.IO.Compression;
using BlossomLib.Modules.Compression;
using BlossomLib.Modules.Parsers;

namespace NetworkMgr
{
/// <summary> Initializes Parsing Functions for PopCap Raw Payloads </summary>

public static class RawPayload
{
// Payload Delimiter: "

private const byte PAYLOAD_WRAP = 0x22;

// Payload Suffix

private const string PAYLOAD_SUFFIX = ",,";

// Check Str

public static bool IsValid(ReadOnlySpan<char> str)
{
var compareMode = StringComparison.Ordinal;

return str.TrimStart().StartsWith("H4sI", compareMode) &&
str.Length >= 28 &&
str.EndsWith(PAYLOAD_SUFFIX, compareMode);

}

// Get Payload Stream

public static void EncodeStream(Stream input, Stream output)
{
int bufferSize = MemoryManager.GetBufferSize(input);
int baseChunks = Base64.GetEncodedLengthUtf8(bufferSize);

using ChunkedMemoryStream baseStream = new(baseChunks);

baseStream.WriteByte(PAYLOAD_WRAP);
Base64.EncodeStream(input, baseStream, true);

baseStream.WriteString(PAYLOAD_SUFFIX);
baseStream.WriteByte(PAYLOAD_WRAP);

baseStream.Seek(0, SeekOrigin.Begin);

int gzChunks = MemoryManager.GetBufferSize(baseStream);
using ChunkedMemoryStream gzStream = new(gzChunks);

GZipCompressor.CompressStream(baseStream, gzStream, CompressionLevel.Fastest);
gzStream.Seek(0, SeekOrigin.Begin);

Base64.EncodeStream(gzStream, output, true);
output.WriteString(PAYLOAD_SUFFIX);
}

// Encode String

public static string EncodeString(ReadOnlySpan<char> str)
{
using ChunkedMemoryStream prStream = new(str.Length);
prStream.WriteString(str);

prStream.Seek(0, SeekOrigin.Begin);

using ChunkedMemoryStream encodedStream = new();
EncodeStream(prStream, encodedStream);

encodedStream.Seek(0, SeekOrigin.Begin);

using var encOwner = encodedStream.ReadString();

return encOwner.ToString();
}

/** <summary> Encodes a Payload File by using Base64 + GZip. </summary>

<param name = "inputPath"> The Path where the File to Encode is Located. </param>
<param name = "outputPath"> The Location where the Encoded File will be Saved. </param> */

public static void EncodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("Payload Encoding Started");

try
{
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

string fileSize = SizeT.FormatSize(inFile.Length);

TraceLogger.WriteActionStart($"Encoding data... ({fileSize})");
EncodeStream(inFile, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encode file");
}

TraceLogger.WriteLine("Payload Encoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Get Plain Stream

public static void DecodeStream(Stream input, Stream output)
{
int gzChunks = MemoryManager.GetBufferSize(input);

using ChunkedMemoryStream gzStream = new(gzChunks);
Base64.DecodeStream(input, gzStream, true, input.Length - PAYLOAD_SUFFIX.Length);

int baseChunks = MemoryManager.GetBufferSize(gzStream);
using ChunkedMemoryStream baseStream = new(baseChunks);

gzStream.Seek(0, SeekOrigin.Begin);
GZipCompressor.DecompressStream(gzStream, baseStream);

baseStream.Seek(1, SeekOrigin.Begin); // Skip Prefix (0x22)
Base64.DecodeStream(baseStream, output, true, baseStream.Length - 4); // Remove trailing chars: ,,"
}

// Decode Payload String

public static string DecodeString(ReadOnlySpan<char> str)
{
using ChunkedMemoryStream prStream = new(str.Length);
prStream.WriteString(str);

if(!str.EndsWith(PAYLOAD_SUFFIX, StringComparison.Ordinal) )
prStream.WriteString(PAYLOAD_SUFFIX); // String was trimmed, so Add suffix again

prStream.Seek(0, SeekOrigin.Begin);

using ChunkedMemoryStream plainStream = new();
DecodeStream(prStream, plainStream);

plainStream.Seek(0, SeekOrigin.Begin);

using var decOwner = plainStream.ReadString();

return decOwner.ToString();
}

/** <summary> Decodes a Payload by using GZip + Base64. </summary>

<param name = "inputPath"> The Path where the File to Decode is Located. </param>
<param name = "outputPath"> The Location where the Decompiled File will be Saved. </param> */

public static void DecodeFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("Payload Decoding Started");

try
{
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

string fileSize = SizeT.FormatSize(inFile.Length);

TraceLogger.WriteActionStart($"Decoding data... ({fileSize})");
DecodeStream(inFile, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decode file");
}

TraceLogger.WriteLine("Payload Decoding Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

}

}