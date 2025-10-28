using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a generic Request Schema that uses Encryption </summary>

public class XResponseEncryptedSchema
{
/** <summary> Gets or Sets a String that Contains the Result Code. </summary>
<returns> The Result Code. </returns> */

[JsonPropertyName("resultCode") ]

public string Result{ get; set; } = "0000";

/** <summary> Gets or Sets the Response Content as an Encrypted String. </summary>
<returns> The Response Content. </returns> */

[JsonPropertyName("content") ]

public string Content{ get; set; }

// ctor

public XResponseEncryptedSchema()
{
}

// ctor 2

public XResponseEncryptedSchema(string result, string content)
{
Result = result;
Content = content;
}

public static readonly JsonSerializerContext Context = new XResponseCryptoContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(XResponseEncryptedSchema) ) ]

public partial class XResponseCryptoContext : JsonSerializerContext
{
}

}