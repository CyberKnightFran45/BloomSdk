using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a generic Request Schema that uses Encryption </summary>

public class XRequestEncryptedSchema : HttpUrlDoc<XRequestEncryptedSchema>
{
/** <summary> Gets or Sets the Request Header. </summary>
<returns> The Request Header. </returns> */

public string Header{ get; set; } = "";

/** <summary> Gets or Sets a MD5 String used for Validating Request. </summary>
<returns> The Md5 String </returns> */

public string Md5{ get; set; } = "";

// ctor

public XRequestEncryptedSchema()
{
Init();
}

// ctor 2

public XRequestEncryptedSchema(string head, string hash)
{
Header = head;
Md5 = hash;

Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => Header);
RegisterGetter(1, () => Md5);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => Header = val);
RegisterSetter(1, val => Md5 = val);
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("head", 0);
RegisterField("md5", 1);
}

/** <summary> Converts a Plain Request to an Encrypted one. </summary>

<param name="plain"> The Plain Request. </param>

<returns> An Encrypted Request Schema. </returns> */

public static XRequestEncryptedSchema FromPlain(XRequestSchema plain)
{
var rawHead = JsonSerializer.SerializeObject(plain.Header, RequestHead.Context);
string head = TWSecurity.CipherData(rawHead, true);

string hash = plain.GetHash();

return new(head, hash);
}

}

}