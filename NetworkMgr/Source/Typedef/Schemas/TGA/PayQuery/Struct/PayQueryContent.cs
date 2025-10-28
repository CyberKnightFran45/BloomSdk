using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Content for PayQuery </summary>

public class PayQueryContent
{
/** <summary> Gets or Sets Info related to the App where the Pay is done. </summary>
<returns> The AppInfo </returns> */

[JsonPropertyName("appInfo") ]

public TwAppInfo AppInfo{ get; set; } = new();

/** <summary> Gets or Sets a List of PayWays available </summary>
<returns> The PayWays. </returns> */

[JsonPropertyName("paywayInfos") ]

public List<PayWayInfo> PayWays{ get; set; } = new();

/// Add PayWay to List

public void AddPayWay(PayWayInfo payWay) => PayWays.Add(payWay);

/// Remove PayWay from List

public void RemovePayWay(int index)
{

if(PayWays.Count > 0)
{
index = Math.Clamp(index, 0, PayWays.Count - 1);

PayWays.RemoveAt(index);
}

}

// ctor

public PayQueryContent()
{
}

public static readonly JsonSerializerContext Context = new PayQuerySubContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(TwAppInfo) ) ]
[JsonSerializable(typeof(PayWayInfo) ) ]
[JsonSerializable(typeof(List<PayWayInfo>) ) ]

[JsonSerializable(typeof(PayQueryContent) ) ]

public partial class PayQuerySubContext : JsonSerializerContext
{
}

}