using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a BuryingPoint </summary>

public class BuryingPoint : ILoggable<BuryingPoint>
{
/** <summary> Gets or Sets a String which containts the current Time. </summary>
<remarks> Format: <c>yyyy-MM-dd HH:mm:ss</c> </remarks>

<returns> The Formatted Time. </returns> */

[JsonPropertyName("formattedTime") ]

public string FormattedTime{ get; set; }

/** <summary> Gets or Sets the Source Platform. </summary>
<returns> The Platform (defaults to Android). </returns> */

[JsonPropertyName("platform") ]

public string Platform{ get; set; } = "Android";

/** <summary> Gets or Sets the App ProcesName. </summary>
<remarks> This Field is Handled from Android Env </remarks>

<returns> The App ProcesName. </returns> */

[JsonPropertyName("appProcessName") ]

public string AppProcessName{ get; set; } = "";

/** <summary> Gets or Sets the App VersionName. </summary>
<remarks> This Field is Handled from Android Env </remarks>

<returns> The Version Name. </returns> */

[JsonPropertyName("versionName") ]

public string VersionName{ get; set; } = "";

/** <summary> Gets or Sets the SecretKey. </summary>
<returns> The SecretKey. </returns> */

[JsonPropertyName("secretKey") ]

public string SecretKey{ get; set; } = "b065a09a9ac7854578b96615a609eb5f";

/** <summary> Gets or Sets the Name of the Event Triggered. </summary>
<returns> The Event Name. </returns> */

[JsonPropertyName("event") ]

public string Event{ get; set; } = "";

/** <summary> Gets or Sets an ID from App Channel. </summary>
<returns> The Channel ID. </returns> */

[JsonPropertyName("channelId") ]

public string ChannelId{ get; set; } = "";

/** <summary> Gets or Sets a Value that represents time between Transaction. </summary>
<returns> The TimeSpace. </returns> */

[JsonPropertyName("timeSpace") ]

public double TimeSpace{ get; set; }

/** <summary> Gets or Sets the User ID </summary>
<returns> The User ID. </returns> */

[JsonPropertyName("userId") ]

public string UserId{ get; set; } = "";

/** <summary> Gets or Sets the Current Network State. </summary>
<remarks> This Field is Handled from Android Env </remarks>

<returns> The Network State. </returns> */

[JsonPropertyName("networkState") ]

public string NetworkState{ get; set; } = "";

/** <summary> Gets or Sets Info related to user Device, such as Brand and Model. </summary>
<remarks> This Field is Handled from Android Env </remarks>

<returns> The Device Info. </returns> */

[JsonPropertyName("deviceInfo") ]

public string DeviceInfo{ get; set; } = "";

// ctor

public BuryingPoint()
{
Key = "10999";

FormattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
TimeSpace = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => FormattedTime);
RegisterGetter(1, () => Key);
RegisterGetter(2, () => Platform);
RegisterGetterRnd(3);
RegisterGetterRnd(4);
RegisterGetterRnd(5);
RegisterGetter(6, () => AppProcessName);
RegisterGetter(7);
RegisterGetter(8, () => VersionName);
RegisterGetter(9, () => SecretKey);
RegisterGetter(10, () => Event);
RegisterGetter(11, () => ChannelId);
RegisterGetterRnd(12);
RegisterGetter(13, TimeSpace.ToString);
RegisterGetter(14, () => UserId);
RegisterGetter(15, () => NetworkState);
RegisterGetter(16, () => DeviceInfo);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => FormattedTime = val);
RegisterSetter(1, val => Key = val);
RegisterSetter(2, val => Platform = val);
RegisterSetter(6, val => AppProcessName = val);
RegisterSetter(8, val => VersionName = val);
RegisterSetter(9, val => SecretKey = val);
RegisterSetter(10, val => Event = val);
RegisterSetter(11, val => ChannelId = val);
RegisterSetter(13, val => TimeSpace = InputHelper.FilterNumber<double>(val) );
RegisterSetter(14, val => UserId = val);
RegisterSetter(15, val => NetworkState = val);
RegisterSetter(16, val => DeviceInfo = val);
}

private static readonly BuryingPoint defaultContent = new();

// Check Fields

public override void CheckFields()
{
FormattedTime ??= defaultContent.FormattedTime;
Key ??= defaultContent.Key;
Platform ??= defaultContent.Platform;
AppProcessName ??= defaultContent.AppProcessName;
VersionName ??= defaultContent.VersionName;
SecretKey ??= defaultContent.SecretKey;
Event ??= defaultContent.Event;
ChannelId ??= defaultContent.ChannelId;
TimeSpace = TimeSpace == 0 ? defaultContent.TimeSpace : TimeSpace;
UserId ??= defaultContent.UserId;
NetworkState ??= defaultContent.NetworkState;
DeviceInfo ??= defaultContent.DeviceInfo;
}

public static readonly JsonSerializerContext Context = new BpContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(BuryingPoint) ) ]

public partial class BpContext : JsonSerializerContext
{
}

}