using System;
using System.IO;

namespace NetworkMgr.Parser.TGA
{
/// <summary> Encodes StoreOrder between JSON and HTTP GET. </summary>

public static class OrderParser
{
// Base URL

private const string BASE_URL = "http://adapi.talkyun.com.cn/ad-aggregation-api/pvz/addpay";

// Convert Order from JSON to HTTP

public static void Encode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Encode Order");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.EncodeHttpGet<StoreOrderRequest>(BASE_URL, inputPath, outputPath, StoreOrderRequest.Context);

TraceLogger.Write("Order Encode Finished");
}

// Convert Order from HTTP to JSON

public static void Decode(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("TGA Started: Decode Order");

TraceLogger.WriteDebug($"{inputPath} --> {outputPath}");
TGAParser.DecodeHttpGet<StoreOrderRequest>(inputPath, outputPath, StoreOrderRequest.Context);

TraceLogger.Write("Order Decode Finished");
}

}

}