using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Credentials for LoginRequestSchema </summary>

public class LoginCredentials
{
/** <summary> Gets or Sets the User Password as a Hash. </summary>
<returns> The User Password. </returns> */

[JsonPropertyName("password") ]

public string Password{ get; set; } = "";

/** <summary> Gets or Sets the Phone Number. </summary>
<returns> The Phone Number. </returns> */

[JsonPropertyName("phone") ]

public string PhoneNumber{ get; set; } = "";

/** <summary> Gets or Sets a Token that belongs to User Account. </summary>
<returns> The Account Token </returns> */

[JsonPropertyName("token") ]

public Guid Token{ get; set; }

// ctor

public LoginCredentials()
{
Token = Guid.NewGuid();
}

public static readonly JsonSerializerContext Context = new LoginCredsContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(LoginCredentials) ) ]

public partial class LoginCredsContext : JsonSerializerContext
{
}

}