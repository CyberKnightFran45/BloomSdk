using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents an Encrypted Schema that Stores Info for UserTerminal </summary>

public class UserTerminalEncryptedSchema : HttpUrlSignedDoc<UserTerminalEncryptedSchema>
{
/** <summary> Gets or Sets the Request Header. </summary>
<returns> The Request Header. </returns> */

public string Header{ get; set; } = "";

/** <summary> Gets or Sets a String which Contains some Terminal Info. </summary>
<returns> Terminal Info. </returns> */

public string TerminalInfo{ get; set; } = "";

/** <summary> Gets or Sets a MD5 String used for Validating Request. </summary>
<returns> The Md5 String </returns> */

public string Md5{ get; set; }

// ctor

public UserTerminalEncryptedSchema()
{
Init();
}

// ctor 2

public UserTerminalEncryptedSchema(string head, string info)
{
Header = head;
TerminalInfo = info;

Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => Header);
RegisterGetter(1, () => TerminalInfo);
RegisterGetter(2, () => Md5);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => Header = val);
RegisterSetter(1, val => TerminalInfo = val);
RegisterSetter(2, val => Md5 = val);
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("head", 0);
RegisterField("userTerminal", 1);
RegisterField("md5", 2);
}

// Setup Digest Context

protected override void SetupDigest()
{
_signer = new TWAppSignHelper<UserTerminalEncryptedSchema>();

_signer.InitGetter( () => Md5);
_signer.InitSetter(val => Md5 = val);

_signer.AddTarget(0, () => Header);
_signer.AddTarget(1, () => TerminalInfo);
}

/** <summary> Encrypts Data from UserTerminal. </summary>

<param name="plain"> The Plain data to Encrypt. </param>

<returns> An Encrypted Schema. </returns> */

public static UserTerminalEncryptedSchema FromPlain(UserTerminalSchema plain)
{
var rawHead = JsonSerializer.SerializeObject(plain.Header, RequestHead.Context);
string head = TWSecurity.CipherData(rawHead, true);

var rawInfo = JsonSerializer.SerializeObject(plain.TerminalInfo, UserTerminalInfo.Context);
string info = TWSecurity.CipherData(rawInfo, true);

return new(head, info);
}

}

}