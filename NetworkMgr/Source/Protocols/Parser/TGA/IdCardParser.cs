namespace NetworkMgr.Parser.TGA
{
/// <summary> Encodes Chinese ID Cards between JSON and HTML. </summary>

public static class IdCardParser
{
// Convert VerifyInfo from JSON to HTML

public static void Encode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Encode IdCard");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.EncodeHttp<UserVerifyInfo>(inputPath, outputPath);

TraceLogger.Write("IdCard Encode Finished");
}

// Convert VerifyInfo from HTML To JSON

public static void Decode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Decode IdCard");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.DecodeHttp<UserVerifyInfo>(inputPath, outputPath, UserVerifyInfo.Context);

TraceLogger.Write("IdCard Decode Finished");
}

}

}