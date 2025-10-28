using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a Request made by User when Using a Redeem Code </summary>

public class RedeemCodeRequest : HttpUrlDoc<RedeemCodeRequest>
{
/** <summary> Gets or Sets the ID of the App where Request is sent. </summary>
<returns> The App ID. </returns> */

[JsonPropertyName("appId") ]

public int AppID{ get; set; } = 109;

/** <summary> Gets or Sets the User Id. </summary>
<returns> The User Id </returns> */

[JsonPropertyName("userId") ]

public string UserId{ get; set; } = "";

/** <summary> Gets or Sets the Redeem Code exchanged. </summary>
<returns> The Redeem Code </returns> */

[JsonPropertyName("activityCode") ]

public string ActivityCode{ get; set; } = "";

/// ctor

public RedeemCodeRequest()
{
Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, AppID.ToString);
RegisterGetter(1, () => UserId);
RegisterGetter(2, () => ActivityCode);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => AppID = InputHelper.FilterNumber<int>(val) );
RegisterSetter(1, val => UserId = val);
RegisterSetter(2, val => ActivityCode = val);
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("appId", 0);
RegisterField("userId", 1);
RegisterField("activityCode", 2);
}

public static readonly JsonSerializerContext Context = new RedeemContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(RedeemCodeRequest) ) ]

public partial class RedeemContext : JsonSerializerContext
{
}

}