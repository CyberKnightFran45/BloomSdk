using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Stores Info about the Different Sorts of String found in a JsonPacket </summary>

public class JsonPacketMap
{
/** <summary> Gets or Sets the Type of Packet Analized. </summary>
<returns> The Packet Type. </returns> */

public string PacketType{ get; set; } = "";

/** <summary> Gets or Sets a Collection of JsonStrings and their Type. </summary>
<returns> The Str Nodes. </returns> */

public Dictionary<string, JsonStrFlags> StrNodes{ get; set; } = new();

// ctor

public JsonPacketMap()
{
}

// ctor 2

public JsonPacketMap(string vType)
{
PacketType = vType;
}

public static readonly JsonSerializerContext Context = new JPacketContext(JsonSerializer.Options);

// Add node

public void Add(string node, JsonStrFlags flags) => StrNodes.Add(node, flags);

// Check Key

public bool Contains(string node) => StrNodes.ContainsKey(node);

// Get Node Flags

public JsonStrFlags GetFlags(string node) => StrNodes[node];
}

// Context for serialization

[JsonSerializable(typeof(JsonPacketMap) ) ]

public partial class JPacketContext : JsonSerializerContext
{
}

}