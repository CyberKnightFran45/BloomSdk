using System.Text.Json.Serialization;
using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a Schema of a Response for TalkWeb Login </summary>

public class LoginResponseSchema
{
/** <summary> Gets or Sets a String that Contains the Result Code. </summary>
<returns> The Result Code. </returns> */

[JsonPropertyName("resultCode") ]

public string Result{ get; set; } = "0000";

/** <summary> Gets or Sets the Response Content. </summary>
<returns> The Response Content. </returns> */

[JsonPropertyName("content") ]

public LoginResponseContent Content{ get; set; }

/// ctor

public LoginResponseSchema()
{
Content = new();
}

// ctor 2

public LoginResponseSchema(string result, LoginResponseContent content)
{
Result = result;
Content = content;
}

public static LoginResponseSchema FromEncrypted(LoginResponseEncryptedSchema encrypted)
{
string rawContent = TWSecurity.CipherData(encrypted.Content, false);
var content = JsonSerializer.DeserializeObject<LoginResponseContent>(rawContent, LoginResponseContent.Context);

return new(encrypted.Result, content);
}

public static readonly JsonSerializerContext Context = new LoginResponseContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(LoginResponseContent) ) ]

[JsonSerializable(typeof(LoginResponseSchema) ) ]

public partial class LoginResponseContext : JsonSerializerContext
{
}

}