namespace NetworkMgr.Parser.TGA
{
/// <summary> Encodes UserInfo between JSON and HTTP. </summary>

public static class UserInfoParser
{
// Convert UserInfo from JSON to HTTP

public static void Encode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Encode UserInfo");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.Encode<UserInfo>(inputPath, outputPath, UserInfo.Context);

TraceLogger.Write("UserInfo Encode Finished");
}

// Convert UserInfo from HTTP to JSON

public static void Decode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Decode UserInfo");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.Decode<UserInfo>(inputPath, outputPath, UserInfo.Context);

TraceLogger.Write("UserInfo Decode Finished");
}

}

}