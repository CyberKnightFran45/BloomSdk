using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a Schema of an Encrypted Response from PvZ2CN Server </summary>

public class ServerResponseEncryptedSchema
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

[JsonPropertyName("e") ]

public string ServerData{ get; set; } = "";

// ctor

public ServerResponseEncryptedSchema()
{
}

// ctor 2

public ServerResponseEncryptedSchema(string response, int status)
{
ResponseType = response;
StatusCode = status;
}

/// <summary> Converts a Plain ServerResponse to an Encrypted Schema. </summary>
/// <param name="plain"> The Plain Server Response to convert. </param>
/// <param name="jsonMap"> Optional JSON Packet Map for encoding. </param>
/// <returns> An Encrypted Response Schema. </returns>

public static ServerResponseEncryptedSchema FromPlain(ServerResponseSchema plain,
PacketCipher encryptor, JsonPacketMap jsonMap = null)
{
string type = plain.ResponseType;
ServerResponseEncryptedSchema encrypted = new(type, plain.StatusCode);

if(jsonMap is not null)
PacketParser.EncodeJsonNodes(plain.ServerData, jsonMap);

string jsonData = JsonSerializer.SerializeObject(plain);
string crypto = encryptor(jsonData, type);

encrypted.ServerData = crypto;

return encrypted;
}

public static readonly JsonSerializerContext Context = new CryptoResponseContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(ServerResponseEncryptedSchema) ) ]

public partial class CryptoResponseContext : JsonSerializerContext
{
}

}