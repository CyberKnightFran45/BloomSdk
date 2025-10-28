using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents Info used for Real-Name User Verification (China) </summary>

public class UserVerifyInfo : HttpUrlSignedDoc<UserVerifyInfo>
{
/** <summary> Gets or Sets the User ID </summary>
<returns> The User ID. </returns> */

[JsonPropertyName("userid") ]

public string UserId{ get; set; } = "";

/** <summary> Gets or Sets the User Name </summary>
<returns> The User ID. </returns> */

[JsonPropertyName("username") ]

public string UserName{ get; set; } = "王元昊";

/** <summary> Gets or Sets the Chinese National Identity Card number. </summary>

<remarks> Must be exactly 18-chars long (GB 11643-1999 standard, no separators): <para>

</para> <c>- First 6 digits: Administrative region code.</c> <para>
</para> <c>- Next 8 digits: Date of birth (YYYYMMDD).</c> <para>
</para> <c>- Next 3 digits: Personal sequence code (odd = male, even = female). </c> </remarks>

<returns> The Chinese ID card. </returns> */

[JsonPropertyName("idCard") ]

public string IdCard{ get; set; } = "330921199303050039";

/** <summary> Gets or Sets the App Channel </summary>
<returns> The App Channel. </returns> */

[JsonPropertyName("channel") ]

public string AppChannel{ get; set; } = "208";

/** <summary> Gets or Sets a Signature obtained from Info. </summary>
<returns> The Signature. </returns> */

[JsonPropertyName("sign") ]

public string Sign{ get; set; } = "<md5>";

// ctor

public UserVerifyInfo()
{
Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => UserId);
RegisterGetter(1, () => UserName);
RegisterGetter(2, () => IdCard);
RegisterGetter(3, () => AppChannel);
RegisterGetter(4, () => Sign);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => UserId = val);
RegisterSetter(1, val => UserName = val);
RegisterSetter(2, val => IdCard = val);
RegisterSetter(3, val => AppChannel = val);
RegisterSetter(4, val => Sign = val);
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("userid", 0);
RegisterField("username", 1);
RegisterField("idCard", 2);
RegisterField("channel", 3);
RegisterField("sign", 4);
}

// Setup Digest Context

protected override void SetupDigest()
{
_signer = new TWSignHelper<UserVerifyInfo>();

_signer.InitGetter( () => Sign);
_signer.InitSetter(val => Sign = val);

_signer.AddTarget(0, () => UserId);
_signer.AddTarget(1, () => UserName);
_signer.AddTarget(2, () => IdCard);
_signer.AddTarget(3, () => AppChannel);
}

public static readonly JsonSerializerContext Context = new IdCardContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(UserVerifyInfo) ) ]

public partial class IdCardContext : JsonSerializerContext
{
}

}