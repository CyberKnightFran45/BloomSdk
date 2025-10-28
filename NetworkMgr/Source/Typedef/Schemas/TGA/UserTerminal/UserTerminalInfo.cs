using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Info for UserTerminalSchema </summary>

public class UserTerminalInfo
{
/** <summary> Gets or Sets the ID of the App. </summary>
<returns> The App ID. </returns> */

[JsonPropertyName("appId") ]

public int AppID{ get; set; } = 109;

/** <summary> Gets or Sets the ID of App Channel. </summary>
<returns> The Channel ID. </returns> */

[JsonPropertyName("channelId") ]

public int ChannelID{ get; set; } = 208;

/** <summary> Gets or Sets the IMEI from User's Phone. </summary>

<remarks> Must be 16-chars long, without Separators </remarks>

<returns> The IMEI </returns> */

[JsonPropertyName("imei") ]

public string IMEI{ get; set; } = new('0', 16);

/** <summary> Gets or Sets the Version of the SDK used. </summary>
<returns> The SDK Version </returns> */

[JsonPropertyName("sdkVersion") ]

public string SdkVersion{ get; set; } = "2.0.0";

/** <summary> Gets or Sets the User Id (Optional). </summary>
<returns> The User Id </returns> */

[JsonPropertyName("userId") ]

public int? UserId{ get; set; }

// ctor

public UserTerminalInfo()
{
}

public static readonly JsonSerializerContext Context = new UserTerminalSubContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(UserTerminalInfo) ) ]

public partial class UserTerminalSubContext : JsonSerializerContext
{
}

}