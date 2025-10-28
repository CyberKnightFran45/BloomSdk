using System.Text.Json.Serialization;
using SexyCryptor;

namespace NetworkMgr
{
/// <summary> Represents a Pay Query Schema </summary>

public class PayQuerySchema
{
/** <summary> Gets or Sets a String that Contains the Result Code. </summary>
<returns> The Result Code. </returns> */

[JsonPropertyName("resultCode") ]

public string Result{ get; set; } = "0000";

/** <summary> Gets or Sets the Response Content. </summary>
<returns> The Response Content. </returns> */

[JsonPropertyName("content") ]

public PayQueryContent Content{ get; set; }

/// Add PayWay to List

public void AddPayWay(PayWayInfo payWay) => Content.AddPayWay(payWay);

/// Add new PayWay

public void NewPayWay() => Content.AddPayWay(new() );

/// Remove PayWay from List

public void RemovePayWay(int index) => Content.RemovePayWay(index);

/// ctor

public PayQuerySchema()
{
Content = new();
}

// ctor 2

public PayQuerySchema(string result, PayQueryContent content)
{
Result = result;
Content = content;
}

public static PayQuerySchema FromEncrypted(PayQueryEncryptedSchema encrypted)
{
string rawContent = TWSecurity.CipherData(encrypted.Content, false);
var content = JsonSerializer.DeserializeObject<PayQueryContent>(rawContent, PayQueryContent.Context);

return new(encrypted.Result, content);
}

public static readonly JsonSerializerContext Context = new PayQueryContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(PayQueryContent) ) ]

[JsonSerializable(typeof(PayQuerySchema) ) ]

public partial class PayQueryContext : JsonSerializerContext
{
}

}