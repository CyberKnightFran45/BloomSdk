using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Contains the Details about TalkWeb App </summary>

public class TwAppInfo
{
/** <summary> Gets or Sets the ID of the App where the Login is made. </summary>
<returns> The App ID. </returns> */

[JsonPropertyName("appId") ]

public int AppID{ get; set; } = 109;

/** <summary> Gets or Sets the App Name. </summary>
<returns> The App Name. </returns> */

[JsonPropertyName("appName") ]

public string AppName{ get; set; } = "植物大战僵尸2高清版";

/** <summary> Gets or Sets the State of the App <c>(1 = Open)</c>. </summary>
<returns> The App State. </returns> */

[JsonPropertyName("state") ]

public string State{ get; set; } = "1";

/** <summary> Gets or Sets an Application Key. </summary>
<returns> The App Key </returns> */

[JsonPropertyName("appKey") ]

public Guid AppKey{ get; set; }

/** <summary> Gets or Sets an Application Key. </summary>
<returns> The App Key </returns> */

[JsonPropertyName("serverKey") ]

public Guid ServerKey{ get; set; }

/** <summary> Gets or Sets the Creation Time of the App as a String. </summary>
<remarks> Format: <c>MMM dd, yyyy hh:mm:ss tt</c> </remarks>

<returns> The Creation Time. </returns> */

[JsonPropertyName("createTime") ]

public string CreationTime{ get; set; }

/** <summary> Gets or Sets a String containing a Validation Date. </summary>
<returns> The Validation Date. </returns> */

[JsonPropertyName("validDate") ]

public string ValidationDate{ get; set; }

/** <summary> Gets or Sets a Callback URL. </summary>
<returns> The Callback URL. </returns> */

[JsonPropertyName("callbackUrl") ]

public string CallbackUrl{ get; set; } = "https://<your_url>";

// ctor

public TwAppInfo()
{
CreationTime = DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt");
}

public static readonly JsonSerializerContext Context = new TwAppContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(TwAppInfo) ) ]

public partial class TwAppContext : JsonSerializerContext
{
}

}