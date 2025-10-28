using BlossomLib.Modules.Parsers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;

namespace NetworkMgr
{
// Parse Network Packets

public static class PacketParser
{
// Get Base64 String

private static string GetBase64(ReadOnlySpan<char> v, bool webSafe)
{
using var rOwner = InputHelper.GetNativeBytes(v);
using var bOwner = Base64.EncodeBytes(rOwner.AsSpan(), webSafe);

return bOwner.ToString();
}

// Encode string node

private static string EncodeStrNode(ReadOnlySpan<char> v, JsonStrFlags strFlags)
{

return strFlags switch
{
JsonStrFlags.Base64 => GetBase64(v, false),
JsonStrFlags.Base64Url => GetBase64(v, true),
JsonStrFlags.RawPayload => RawPayload.EncodeString(v),
_ => new(v)
};

}

// Encode list

public static void EncodeList64(List<object> list, JsonPacketMap nodesDebug, string currentPath = "")
{

for (int i = 0; i < list.Count; i++)
{
string newPath = $"{currentPath}[{i}]";

if(list[i] is ExpandoObject expando)
EncodeExpandObj64(expando, nodesDebug, newPath);
                
else if(list[i] is List<object> subList)
EncodeList64(subList, nodesDebug, newPath);

else if(list[i] is string val && nodesDebug?.Contains(newPath) == true)
list[i] = EncodeStrNode(val, nodesDebug.GetFlags(newPath) );
                
}

}

// Encode ExpandoObject

private static void EncodeExpandObj64(ExpandoObject node, JsonPacketMap nodesDebug, string currentPath = "")
{
var dict = (IDictionary<string, object>)node;

foreach(var key in dict.Keys.ToList() )
{
string newPath = string.IsNullOrEmpty(currentPath) ? key : $"{currentPath}.{key}";

if(dict[key] is ExpandoObject subExpando)
EncodeExpandObj64(subExpando, nodesDebug, newPath);
                
else if(dict[key] is List<object> list)
EncodeList64(list, nodesDebug, newPath);

else if (dict[key] is string val && nodesDebug?.Contains(newPath) == true)
dict[key] = EncodeStrNode(val, nodesDebug.GetFlags(newPath) );

}

}

// Encode JSON Nodes
		
public static void EncodeJsonNodes(ExpandoObject jsonObj, JsonPacketMap debugMap, string currentPath = "")
{

if(jsonObj is null)
return;

EncodeExpandObj64(jsonObj, debugMap, currentPath);
}

// Decode Str Node

private static string DecodeStrNode(ReadOnlySpan<char> val, out JsonStrFlags flags)
{
string decoded = val.ToString();

if(val.Length == 0)
flags = JsonStrFlags.EmptyString;

else if(val.IsWhiteSpace() )
flags = JsonStrFlags.WhiteSpace;

else if(decimal.TryParse(val, out _) )
flags = JsonStrFlags.Number;

else if(DateTime.TryParseExact(val, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, default, out _) )
flags = JsonStrFlags.DateTime;

// Payload Check

else if(RawPayload.IsValid(val) )
{
flags = JsonStrFlags.RawPayload;
decoded = RawPayload.DecodeString(val);
}

// Base64 Check (Standard)

else if(Base64.IsStandarLike(val, out string basePlain) )
{
flags = JsonStrFlags.Base64;
decoded = basePlain;
}

// Base64 Check (Web-safe)

else if(Base64.IsUrlLike(val, out string urlPlain) )
{
flags = JsonStrFlags.Base64Url;
decoded = urlPlain;
}

else
flags = JsonStrFlags.PlainText;

return decoded;
}

// Check if Node is Raw Json or a Encoded Str

private static bool IsRawJson(JsonStrFlags flags)
{

return flags != JsonStrFlags.Base64 &&
flags != JsonStrFlags.Base64Url &&
flags != JsonStrFlags.RawPayload;

}

// Decode Elements from List

public static void DecodeList64(List<object> list, JsonPacketMap nodesDebug = null, string currentPath = "")
{

for(int i = 0; i < list.Count; i++)
{
string newPath = $"{currentPath}[{i}]";

if(list[i] is string val)
{
string parsedNode = DecodeStrNode(val, out JsonStrFlags strFlags);

try
{
var jToken = JToken.Parse(parsedNode);

bool isJson = IsRawJson(strFlags);

// JToken is an Object

if(jToken.Type == JTokenType.Object)
{
var expando = ExpandObjPlugin.ToExpandoObject(jToken.ToObject<JObject>() );
DecodeExpandObj64(expando, nodesDebug, newPath);

if(isJson)
strFlags = JsonStrFlags.JsonObject;

list[i] = expando;
}

// JToken is an Array

else if(jToken.Type == JTokenType.Array)
{
var array = ExpandObjPlugin.ConvertJArray(jToken.ToObject<JArray>() );
DecodeList64(array, nodesDebug, newPath);

if(isJson)
strFlags = JsonStrFlags.JsonArray;

list[i] = array;
}

else
list[i] = parsedNode;

}

catch
{
list[i] = parsedNode;
}

nodesDebug?.Add(newPath, strFlags);
}

else if(list[i] is ExpandoObject expando)
DecodeExpandObj64(expando, nodesDebug, newPath);

else if(list[i] is List<object> subList)
DecodeList64(subList, nodesDebug, newPath);

}

}

// Decode Json Nodes (Internal)

private static void DecodeExpandObj64(ExpandoObject node, JsonPacketMap nodesDebug = null,
string currentPath = "")
{
var dict = (IDictionary<string, object>)node;

foreach(var key in dict.Keys.ToList() )
{
string newPath = string.IsNullOrEmpty(currentPath) ? key : $"{currentPath}.{key}";
 
if(dict[key] is ExpandoObject subExpando)
DecodeExpandObj64(subExpando, nodesDebug, newPath);

else if(dict[key] is List<object> list)
DecodeList64(list, nodesDebug, newPath);
		
else if(dict[key] is string val)
{
string parsedNode = DecodeStrNode(val, out JsonStrFlags strFlags);

try
{
var jToken = JToken.Parse(parsedNode);

bool isJson = IsRawJson(strFlags);

// JToken is Child Node

if(jToken.Type == JTokenType.Object)
{
var expando = ExpandObjPlugin.ToExpandoObject(jToken.ToObject<JObject>() );
DecodeExpandObj64(expando, nodesDebug, newPath);

if(isJson)
strFlags = JsonStrFlags.JsonObject;

dict[key] = expando;
}

// JToken is an Array

else if(jToken.Type == JTokenType.Array)
{
var array = ExpandObjPlugin.ConvertJArray(jToken.ToObject<JArray>() );
DecodeList64(array, nodesDebug, newPath);

if(isJson)
strFlags = JsonStrFlags.JsonArray;

dict[key] = array;
}	

}

catch
{
dict[key] = parsedNode;
}

nodesDebug?.Add(newPath, strFlags);
}

}

}

// Decode Json Nodes

public static void DecodeJsonNodes(ExpandoObject jsonObj, JsonPacketMap debugMap = null,
string currentPath = "")
{

if(jsonObj is null)
return;

DecodeExpandObj64(jsonObj, debugMap, currentPath);
}

// Decode JToken as Array or Object

public static dynamic DecodeToken(JToken parsedJson, JsonPacketMap jsonMap)
{
dynamic jsonData;

if(parsedJson.Type == JTokenType.Array)
{
var jArr = parsedJson.ToObject<JArray>();
jsonData = ExpandObjPlugin.ConvertJArray(jArr);

PacketParser.DecodeList64(jsonData, jsonMap);
}

else
{
var jObj = parsedJson.ToObject<JObject>();
jsonData = ExpandObjPlugin.ToExpandoObject(jObj);

PacketParser.DecodeJsonNodes(jsonData, jsonMap);
}

return jsonData;
}

}

}