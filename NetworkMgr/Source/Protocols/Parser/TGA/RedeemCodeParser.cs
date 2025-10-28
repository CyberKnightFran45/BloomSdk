using System;
using System.IO;

namespace NetworkMgr.Parser.TGA
{
/// <summary> Encodes RedeemCodes between JSON and HTTP GET. </summary>

public static class RedeemCodeParser
{
// Base URL

private const string BASE_URL = "http://payment.talkyun.com.cn/PayMobilePbs/servlet/JoinActivity";

// Convert RedeemCode from JSON to HTTP

public static void Encode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Encode RedeemCode");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.EncodeHttpGet<RedeemCodeRequest>(BASE_URL, inputPath, outputPath, RedeemCodeRequest.Context);

TraceLogger.Write("RedeemCode Encode Finished");
}

// Convert RedeemCode from HTTP to JSON

public static void Decode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Decode RedeemCode");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.DecodeHttpGet<RedeemCodeRequest>(inputPath, outputPath, RedeemCodeRequest.Context);

TraceLogger.Write("RedeemCode Decode Finished");
}

}

}