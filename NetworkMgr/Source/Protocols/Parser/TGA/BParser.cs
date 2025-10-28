namespace NetworkMgr.Parser.TGA
{
/// <summary> Encodes BuryingPoints between JSON and HTTP. </summary>

public static class BParser
{
// Convert BP from JSON to HTTP

public static void Encode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Encode BuryingPoint");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.Encode<BuryingPoint>(inputPath, outputPath, BuryingPoint.Context);

TraceLogger.Write("BP Encode Finished");
}

// Convert BP from HTTP To JSON

public static void Decode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Decode BuryingPoint");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.Decode<BuryingPoint>(inputPath, outputPath, BuryingPoint.Context);

TraceLogger.Write("BP Decode Finished");
}

}

}