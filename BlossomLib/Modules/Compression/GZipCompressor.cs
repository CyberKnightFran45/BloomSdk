﻿using System;
using System.IO;
using System.IO.Compression;

namespace BlossomLib.Modules.Compression
{
/// <summary> Initializes Compression Tasks for Files by using the GZip algorithm. </summary>

public static class GZipCompressor
{
// Get GZip Stream

public static void CompressStream(Stream input, Stream output, CompressionLevel level,
long maxBytes = -1, Action<long, long> progressCallback = null)
{
using GZipStream gzStream = new(output, level, true);

FileManager.Process(input, gzStream, maxBytes, progressCallback);
}

/** <summary> Compresses the Contents of a File by using the GZip Algorithm. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param> */

public static void CompressFile(string inputPath, string outputPath, CompressionLevel level,
Action<long, long> progressCallback = null)
{
TraceLogger.Init();
TraceLogger.WriteLine("GZip Compression Started");

long originalSize = 0;

try
{
PathHelper.AddExtension(ref outputPath, ".gz");
TraceLogger.WriteDebug($"{inputPath} --> {outputPath} (Level: {level})");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

originalSize = inFile.Length;
string fileSize = SizeT.FormatSize(originalSize);

TraceLogger.WriteActionStart($"Compressing data... ({fileSize})");
CompressStream(inFile, outFile, level, -1, progressCallback);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Compress file");
}

TraceLogger.WriteLine("GZip Compression Finished");

var outSize = FileManager.GetFileSize(outputPath);
string sizeCompressed = SizeT.FormatSize(outSize);

var ratio = (double)outSize / originalSize;
TraceLogger.WriteInfo($"Output Size: {sizeCompressed} (Ratio: {ratio:P2})", false);
}

// Write GZip

public static void DecompressStream(Stream input, Stream output, long maxBytes = -1,
Action<long, long> progressCallback = null)
{
using GZipStream decompressor = new(input, CompressionMode.Decompress, true);

FileManager.Process(decompressor, output, maxBytes, progressCallback);
}

/** <summary> Decompresses the Contents of a File by using the GZip Algorithm. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed File will be Saved. </param> */

public static void DecompressFile(string inputPath, string outputPath,
Action<long, long> progressCallback = null)
{
TraceLogger.Init();
TraceLogger.WriteLine("GZip Decompression Started");

try
{
TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

string fileSize = SizeT.FormatSize(inFile.Length);
TraceLogger.WriteActionStart($"Decompressing data... ({fileSize})");

DecompressStream(inFile, outFile, -1, progressCallback);
TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decompress file");
}

TraceLogger.WriteLine("GZip Decompression Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

}

}