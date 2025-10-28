using System.Text.Json.Serialization;
using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a Schema of a Request for TalkWeb Login </summary>

public class LoginRequestSchema
{
/** <summary> Gets or Sets the Request Header. </summary>
<returns> The Request Header. </returns> */

[JsonPropertyName("head") ]

public RequestHead Header{ get; set; }

/** <summary> Gets or Sets the Credentials sent by User. </summary>
<returns> The User Credentials. </returns> */

[JsonPropertyName("login") ]

public LoginCredentials Credentials{ get; set; }

/// ctor

public LoginRequestSchema()
{
Header = new();
Credentials = new();
}

// ctor 2

public LoginRequestSchema(RequestHead head, LoginCredentials info)
{
Header = head;
Credentials = info;
}

public static LoginRequestSchema FromEncrypted(LoginRequestEncryptedSchema encrypted)
{
string rawHead = TWSecurity.CipherData(encrypted.Header, false);
var head = JsonSerializer.DeserializeObject<RequestHead>(rawHead, RequestHead.Context);

string rawInfo = TWSecurity.CipherData(encrypted.Credentials, false);
var info = JsonSerializer.DeserializeObject<LoginCredentials>(rawInfo, LoginCredentials.Context);

return new(head, info);
}

public static readonly JsonSerializerContext Context = new LoginRequestContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(RequestHead) ) ]
[JsonSerializable(typeof(LoginCredentials) ) ]

[JsonSerializable(typeof(LoginRequestSchema) ) ]

public partial class LoginRequestContext : JsonSerializerContext
{
}

}