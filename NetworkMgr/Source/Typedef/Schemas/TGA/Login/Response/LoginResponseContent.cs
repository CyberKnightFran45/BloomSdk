using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Content for LoginResponse </summary>

public class LoginResponseContent
{
/** <summary> Gets or Sets the User Id. </summary>
<returns> The User Id </returns> */

[JsonPropertyName("userId") ]

public int UserId{ get; set; }

/** <summary> Gets or Sets the User Nickname. </summary>
<returns> The User Nick. </returns> */

[JsonPropertyName("userNick") ]

public string Nickname{ get; set; } = "Nick";

/** <summary> Gets or Sets a ID from TW Account (Optional). </summary>
<returns> The TalkWeb ID. </returns> */

[JsonPropertyName("talkwebUserId") ]

public string TalkWebID{ get; set; }

/** <summary> Gets or Sets a Token that belongs to User Account. </summary>
<returns> The Account Token </returns> */

[JsonPropertyName("token") ]

public Guid Token{ get; set; }

// ctor

public LoginResponseContent()
{
Token = Guid.NewGuid();
}

public static readonly JsonSerializerContext Context = new LoginResponseSubContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(LoginResponseContent) ) ]

public partial class LoginResponseSubContext : JsonSerializerContext
{
}

}