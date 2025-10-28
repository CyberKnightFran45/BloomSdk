using System.Text.Json.Serialization;
using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a Schema that Stores Info for UserTerminal </summary>

public class UserTerminalSchema : TWSignHelper<UserTerminalSchema>
{
/** <summary> Gets or Sets the Request Header. </summary>
<returns> The Request Header. </returns> */

[JsonPropertyName("head") ]

public RequestHead Header{ get; set; }

/** <summary> Gets or Sets some Terminal Info. </summary>
<returns> Terminal Info. </returns> */

[JsonPropertyName("userTerminal") ]

public UserTerminalInfo TerminalInfo{ get; set; }

/// ctor

public UserTerminalSchema()
{
Header = new();
TerminalInfo = new();
}

// ctor 2

public UserTerminalSchema(RequestHead head, UserTerminalInfo info)
{
Header = head;
TerminalInfo = info;
}

public static UserTerminalSchema FromEncrypted(UserTerminalEncryptedSchema encrypted)
{
string rawHead = TWSecurity.CipherData(encrypted.Header, false);
var head = JsonSerializer.DeserializeObject<RequestHead>(rawHead, RequestHead.Context);

string rawInfo = TWSecurity.CipherData(encrypted.TerminalInfo, false);
var info = JsonSerializer.DeserializeObject<UserTerminalInfo>(rawInfo, UserTerminalInfo.Context);

return new(head, info);
}

public static readonly JsonSerializerContext Context = new UserTerminalContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(RequestHead) ) ]
[JsonSerializable(typeof(LoginCredentials) ) ]

[JsonSerializable(typeof(LoginRequestSchema) ) ]

public partial class UserTerminalContext : JsonSerializerContext
{
}

}