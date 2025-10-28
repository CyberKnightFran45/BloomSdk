using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Contains the Details of a PayWay </summary>

public class PayWayInfo
{
/** <summary> Gets or Sets a PayWay ID. </summary>
<returns> The Payway ID. </returns> */

[JsonPropertyName("payWayId") ]

public string PayWayId{ get; set; } = "<Id>";

/** <summary> Gets or Sets a PayWay Name. </summary>
<returns> The Payway Name. </returns> */

[JsonPropertyName("payWayId") ]

public string PayWayName{ get; set; } = "<Name>";

/** <summary> Gets or Sets a Callback URL used when Paying. </summary>
<returns> The Callback URL. </returns> */

[JsonPropertyName("payCallbackUrl") ]

public string CallbackUrl{ get; set; } = "https://<pay_url>";

/** <summary> Gets or Sets a Callback URL for Pay Request (Optional). </summary>
<returns> The Request URL. </returns> */

[JsonPropertyName("payRequestUrl") ]

public string RequestUrl{ get; set; }

/** <summary> Gets or Sets the Pay Type. </summary>
<returns> The Pay Type. </returns> */

[JsonPropertyName("payType") ]

public string PayType{ get; set; } = "<Type>";

/** <summary> Gets or Sets a Logo to Display. </summary>
<returns> A URL to the Asset. </returns> */

[JsonPropertyName("logo") ]

public string Logo{ get; set; } = "https://<your_logo>";

/** <summary> Gets or Sets the Creation Time of the PayWay. </summary>
<remarks> Format: <c>MMM dd, yyyy hh:mm:ss tt</c> </remarks>

<returns> The Creation Time. </returns> */

[JsonPropertyName("createTime") ]

public string CreationTime{ get; set; }

/** <summary> Gets or Sets some special Config as a Json String (Optional). </summary>
<returns> The Json Config. </returns> */

[JsonPropertyName("configJson") ]

public string Config{ get; set; }

/** <summary> Gets or Sets the Pay Style. </summary>
<returns> The Pay Style. </returns> */

[JsonPropertyName("payStyle") ]

public string PayStyle{ get; set; } = "2";

// ctor

public PayWayInfo()
{
CreationTime = DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt");
}

public static readonly JsonSerializerContext Context = new PayWayContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(PayWayInfo) ) ]

public partial class PayWayContext : JsonSerializerContext
{
}

}