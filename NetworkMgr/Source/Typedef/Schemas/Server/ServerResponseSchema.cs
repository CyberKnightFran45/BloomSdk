using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace NetworkMgr
{
/// <summary> Represents a Schema of a Response from PvZ2CN Server </summary>

public class ServerResponseSchema
{
/** <summary> Gets or Sets the Type of Response generated. </summary>
<returns> The Response Type. </returns> */

[JsonPropertyName("i") ]

public string ResponseType{ get; set; } = "";

/** <summary> Gets or Sets a Number that represents the Response Status. </summary>
<returns> The Response Code. </returns> */

[JsonPropertyName("r") ]

public int StatusCode{ get; set; }

/** <summary> Gets or Sets the Data delivered by Server. </summary>
<returns> The Server Data. </returns> */

[JsonPropertyName("d") ]

public dynamic ServerData{ get; set; } = new List<object>();

/// ctor

public ServerResponseSchema()
{
}

// ctor 2

public ServerResponseSchema(string type, int status, dynamic data)
{
ResponseType = type;
StatusCode = status;

ServerData = data;
}

// Read Json

public static ServerResponseSchema Read(string sourcePath)
{
PathHelper.EnsurePathExists(Path.GetDirectoryName(sourcePath) );

if(!File.Exists(sourcePath) || FileManager.FileIsEmpty(sourcePath) )
return null;

string rawJson = File.ReadAllText(sourcePath);
var parsedJson = JObject.Parse(rawJson); // Alternative for Unk JSON Struct

var type = (string)parsedJson["i"];
var status = (int?)parsedJson["r"] ?? -1;

var subData = parsedJson["d"]?.ToObject<JObject>();
dynamic data = ExpandObjPlugin.ToExpandoObject(subData);

return new(type, status, data);
}

public static ServerResponseSchema FromEncrypted(ServerResponseEncryptedSchema encrypted, 
PacketCipher decryptor, out JsonPacketMap jsonMap)
{
string rawJson = decryptor(encrypted.ServerData, encrypted.ResponseType);
var parsedJson = JObject.Parse(rawJson);

var type = (string)parsedJson["i"];
var status = (int?)parsedJson["r"] ?? -1;

jsonMap = new(type);

var subData = parsedJson["d"];
dynamic data = PacketParser.DecodeToken(subData, jsonMap);

return new(type, status, data);
}

}

}