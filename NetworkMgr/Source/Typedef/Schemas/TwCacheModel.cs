using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a TW Cache </summary>

public class TwCacheModel
{
/** <summary> Gets or Sets a String which contains the Cache Data. </summary>

<returns> The Cache Data. </returns> */

[JsonPropertyName("data") ]

public string Data{ get; set; }

/** <summary> Gets or Sets an UID from User's Device. </summary>
<returns> The User ID. </returns> */

[JsonPropertyName("key") ]

public string Key{ get; set; }

// ctor

public TwCacheModel()
{
}

// ctor 2

public TwCacheModel(string data)
{
Data = data;
}

// ctor 3

public TwCacheModel(string data, string key)
{
Data = data;
Key = key;
}

public static readonly JsonSerializerContext Context = new TwCacheContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(TwCacheModel) ) ]

public partial class TwCacheContext : JsonSerializerContext
{
}

}