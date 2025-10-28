using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents some User Info </summary>

public class UserInfo : ILoggable<UserInfo>
{
/** <summary> Gets or Sets a String which containts the current Time. </summary>
<returns> The Formatted Time. </returns> */

[JsonPropertyName("dateString") ]

public string FormattedTime{ get; set; } = "";

/** <summary> Gets or Sets the Source Platform. </summary>
<returns> The Platform (defaults to Android). </returns> */

[JsonPropertyName("platform") ]

public string Platform{ get; set; } = "Android";

/** <summary> Gets or Sets the Device ID. </summary>
<remarks> This Field is Handled from Android Env </remarks>

<returns> The Device ID. </returns> */

[JsonPropertyName("deviceid") ]

public string DeviceID{ get; set; } = "";

/** <summary> Gets or Sets the Character ID </summary>
<returns> The Character ID. </returns> */

[JsonPropertyName("characterid") ]

public string CharacterId{ get; set; } = "";

/** <summary> Gets or Sets the User ID </summary>
<returns> The User ID. </returns> */

[JsonPropertyName("userid") ]

public string UserId{ get; set; } = "";

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

/** <summary> Gets or Sets the User Age </summary>
<returns> The User Age. </returns> */

[JsonPropertyName("age") ]

public int Age{ get; set; } = 21;

/** <summary> Gets or Sets the Current Network State. </summary>
<remarks> This Field is Handled from Android Env </remarks>

<returns> The Network State. </returns> */

[JsonPropertyName("networkState") ]

public string NetworkState{ get; set; } = "";

// ctor

public UserInfo()
{
Key = "10078";
FormattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

InitGetters();
InitSetters();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => FormattedTime);
RegisterGetter(1, () => Key);
RegisterGetter(2, () => Platform);
RegisterGetter(3, () => DeviceID);
RegisterGetter(4, () => CharacterId);
RegisterGetter(5, () => UserId);
RegisterGetter(6, () => AppProcessName);
RegisterGetter(7);
RegisterGetter(8, () => VersionName);
RegisterGetter(9);
RegisterGetter(10, Age.ToString);
RegisterGetter(11, () => NetworkState);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => FormattedTime = val);
RegisterSetter(1, val => Key = val);
RegisterSetter(2, val => Platform = val);
RegisterSetter(3, val => DeviceID = val);
RegisterSetter(4, val => CharacterId = val);
RegisterSetter(5, val => UserId = val);
RegisterSetter(6, val => AppProcessName = val);
RegisterSetter(8, val => VersionName = val);
RegisterSetter(10, val => Age = InputHelper.FilterNumber<int>(val) );
RegisterSetter(11, val => NetworkState = val);
}

private static readonly UserInfo defaultInfo = new();

// Check Fields

public override void CheckFields()
{
FormattedTime ??= defaultInfo.FormattedTime;
Key ??= defaultInfo.Key;
Platform ??= defaultInfo.Platform;
DeviceID ??= defaultInfo.DeviceID;
CharacterId ??= defaultInfo.CharacterId;
UserId ??= defaultInfo.UserId;
AppProcessName ??= defaultInfo.AppProcessName;
VersionName ??= defaultInfo.VersionName;
NetworkState ??= defaultInfo.NetworkState;
}

public static readonly JsonSerializerContext Context = new UserInfoContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(UserInfo) ) ]

public partial class UserInfoContext : JsonSerializerContext
{
}

}