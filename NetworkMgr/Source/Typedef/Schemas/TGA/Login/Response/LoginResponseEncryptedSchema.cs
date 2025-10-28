using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a Schema of an Encrypted Response for TalkWeb Login </summary>

public class LoginResponseEncryptedSchema : XResponseEncryptedSchema
{
// ctor 1

public LoginResponseEncryptedSchema() : base()
{
}

// ctor 2

public LoginResponseEncryptedSchema(string result, string content) : base(result, content)
{
}

/** <summary> Converts a Plain Response to an Encrypted Schema. </summary>

<param name="plain"> The Response to convert. </param>

<returns> An Encrypted Response. </returns> */

public static LoginResponseEncryptedSchema FromPlain(LoginResponseSchema plain)
{
var rawContent = JsonSerializer.SerializeObject(plain.Content, LoginResponseContent.Context);
string content = TWSecurity.CipherData(rawContent, true);

return new(plain.Result, content);
}

}

}