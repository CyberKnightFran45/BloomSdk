using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a Request Header that contains Info about the Emissor </summary>

public class RequestHead
{
/** <summary> Gets or Sets the ID of the App where the Login is made. </summary>
<returns> The App ID. </returns> */

[JsonPropertyName("appId") ]

public int AppID{ get; set; } = 109;

/** <summary> Gets or Sets the ID of the Channel where the Login is sent. </summary>
<returns> The Channel ID. </returns> */

[JsonPropertyName("channelId") ]

public int ChannelID{ get; set; } = 208;

/** <summary> Gets or Sets the Version of the SDK used. </summary>
<returns> The SDK Version </returns> */

[JsonPropertyName("sdkVersion") ]

public string SdkVersion{ get; set; } = "2.0.0";

// ctor

public RequestHead()
{
}

public static readonly JsonSerializerContext Context = new RequestHeadContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(RequestHead) ) ]

public partial class RequestHeadContext : JsonSerializerContext
{
}

}