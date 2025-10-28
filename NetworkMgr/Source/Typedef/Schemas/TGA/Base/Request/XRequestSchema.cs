using System.Text.Json.Serialization;
using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a generic Schema </summary>

public class XRequestSchema : TWSignHelper<XRequestSchema>
{
/** <summary> Gets or Sets the Request Header. </summary>
<returns> The Request Header. </returns> */

[JsonPropertyName("head") ]

public RequestHead Header{ get; set; }

/** <summary> Gets or Sets a Hash from Credentials. </summary>
<returns> The Hash. </returns> */

private string Hash;

/// ctor

public XRequestSchema()
{
Header = new();

SetupDigest();
}

// ctor 2

public XRequestSchema(RequestHead head)
{
Header = head;

SetupDigest();
}

// Setup Digest Context

private void SetupDigest()
{
InitGetter( () => Hash);
InitSetter(val => Hash = val);

AddTarget(0, Header.AppID.ToString);
}

// Get Hash

public string GetHash()
{
CheckSign();

return Hash;
}

public static XRequestSchema FromEncrypted(XRequestEncryptedSchema encrypted)
{
string rawHead = TWSecurity.CipherData(encrypted.Header, false);
var head = JsonSerializer.DeserializeObject<RequestHead>(rawHead, RequestHead.Context);

return new(head);
}

public static readonly JsonSerializerContext Context = new XRequestContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(RequestHead) ) ]

[JsonSerializable(typeof(XRequestSchema) ) ]

public partial class XRequestContext : JsonSerializerContext
{
}

}