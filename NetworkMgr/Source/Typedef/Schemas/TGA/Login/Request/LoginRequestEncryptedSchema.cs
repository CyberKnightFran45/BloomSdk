using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a Schema of an Encrypted Request for TalkWeb Login </summary>

public class LoginRequestEncryptedSchema : HttpUrlSignedDoc<LoginRequestEncryptedSchema>
{
/** <summary> Gets or Sets the Request Header. </summary>
<returns> The Request Header. </returns> */

public string Header{ get; set; } = "";

/** <summary> Gets or Sets the Credentials sent by User. </summary>
<returns> The User Credentials. </returns> */

public string Credentials{ get; set; } = "";

/** <summary> Gets or Sets a MD5 String used for Validating Request. </summary>
<returns> The Md5 String </returns> */

public string Md5{ get; set; }

// ctor

public LoginRequestEncryptedSchema()
{
Init();
}

// ctor 2

public LoginRequestEncryptedSchema(string head, string info)
{
Header = head;
Credentials = info;

Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => Header);
RegisterGetter(1, () => Credentials);
RegisterGetter(2, () => Md5);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => Header = val);
RegisterSetter(1, val => Credentials = val);
RegisterSetter(2, val => Md5 = val);
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("head", 0);
RegisterField("login", 1);
RegisterField("md5", 2);
}

// Setup Digest Context

protected override void SetupDigest()
{
_signer = new TWAppSignHelper<LoginRequestEncryptedSchema>();

_signer.InitGetter( () => Md5);
_signer.InitSetter(val => Md5 = val);

_signer.AddTarget(0, () => Header);
_signer.AddTarget(1, () => Credentials);
}

/** <summary> Converts a Plain ClientRequest to an Encrypted Schema. </summary>

<param name="plain"> The Plain ClientRequest to convert. </param>

<returns> An Encrypted Request Schema. </returns> */

public static LoginRequestEncryptedSchema FromPlain(LoginRequestSchema plain)
{
var rawHead = JsonSerializer.SerializeObject(plain.Header, RequestHead.Context);
string head = TWSecurity.CipherData(rawHead, true);

var rawInfo = JsonSerializer.SerializeObject(plain.Credentials, LoginCredentials.Context);
string info = TWSecurity.CipherData(rawInfo, true);

return new(head, info);
}

}

}