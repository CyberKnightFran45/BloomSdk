using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a Schema of an Encrypted Pay Query </summary>

public class PayQueryEncryptedSchema : XResponseEncryptedSchema
{
// ctor

public PayQueryEncryptedSchema() : base()
{
}

// ctor 2

public PayQueryEncryptedSchema(string result, string content) : base(result, content)
{
}

/** <summary> Converts a Plain Response to an Encrypted Schema. </summary>

<param name="plain"> The Response to convert. </param>

<returns> An Encrypted Response. </returns> */

public static PayQueryEncryptedSchema FromPlain(PayQuerySchema plain)
{
var rawContent = JsonSerializer.SerializeObject(plain.Content, PayQueryContent.Context);
string content = TWSecurity.CipherData(rawContent, true);

return new(plain.Result, content);
}

}

}