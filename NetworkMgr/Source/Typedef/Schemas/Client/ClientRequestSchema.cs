using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace NetworkMgr
{
/// <summary> Represents a Schema of a Request from PvZ2CN Client </summary>

public class ClientRequestSchema
{
/** <summary> Gets or Sets the Type of Response generated. </summary>
<returns> The Response Type. </returns> */

[JsonPropertyName("req") ]

public string RequestType{ get; set; } = "";

/** <summary> Gets or Sets the Data sent by Client. </summary>
<returns> The Client Data. </returns> */

[JsonPropertyName("d") ]

public dynamic ClientData{ get; set; } = new List<object>();

/** <summary> Gets or Sets the Version of the Encryption Algorithm used. </summary>
<returns> The Encryption Version. </returns> */

[JsonPropertyName("ev") ]

public int EncryptionVer{ get; set; }

/// ctor

public ClientRequestSchema()
{
}

// ctor 2

public ClientRequestSchema(string request, int ver, dynamic data)
{
RequestType = request;
EncryptionVer = ver;

ClientData = data;
}

// Read Json

public static ClientRequestSchema Read(string sourcePath)
{
PathHelper.EnsurePathExists(Path.GetDirectoryName(sourcePath) );

if(!File.Exists(sourcePath) || FileManager.FileIsEmpty(sourcePath) )
return null;

string rawJson = File.ReadAllText(sourcePath);
var parsedJson = JObject.Parse(rawJson); // Alternative for Unk JSON Struct

var type = (string)parsedJson["req"];
var ver = (int?)parsedJson["ev"] ?? -1;

var subData = parsedJson["d"]?.ToObject<JObject>();
dynamic data = ExpandObjPlugin.ToExpandoObject(subData);

return new(type, ver, data);
}

// Convert from Encrypted Schema

public static ClientRequestSchema FromEncrypted(ClientRequestEncryptedSchema encrypted,
PacketCipher decryptor, out JsonPacketMap jsonMap)
{
string type = encrypted.RequestType;

jsonMap = new(type);

string rawJson = decryptor(encrypted.ClientData, type);
var parsedJson = JToken.Parse(rawJson);

dynamic data = PacketParser.DecodeToken(parsedJson, jsonMap);

return new(type, encrypted.EncryptionVer, data);
}

}

}