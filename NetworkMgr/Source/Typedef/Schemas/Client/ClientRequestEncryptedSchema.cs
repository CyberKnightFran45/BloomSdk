namespace NetworkMgr
{
/// <summary> Represents a Schema of an Encrypted Request from PvZ2CN Client </summary>

public class ClientRequestEncryptedSchema : HttpMultipartDoc<ClientRequestEncryptedSchema>
{
/** <summary> Gets or Sets the Type of Resquest made. </summary>
<returns> The Request Type. </returns> */

public string RequestType{ get; set; } = "";

/** <summary> Gets or Sets the Data sent by Client. </summary>
<returns> The Client Data. </returns> */

public string ClientData{ get; set; } = "";

/** <summary> Gets or Sets the Version of the Encryption Algorithm used. </summary>
<returns> The Encryption Version. </returns> */

public int EncryptionVer{ get; set; }

// ctor

public ClientRequestEncryptedSchema()
{
Init();
}

// ctor 2

public ClientRequestEncryptedSchema(string request, int ver)
{
RequestType = request;
EncryptionVer = ver;

Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => RequestType);
RegisterGetter(1, () => ClientData);
RegisterGetter(2, () => $"{EncryptionVer}");
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => RequestType = val);
RegisterSetter(1, val => ClientData = val);
RegisterSetter(2, val => EncryptionVer = InputHelper.FilterNumber<int>(val) );
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("req", 0);
RegisterField("e", 1);
RegisterField("ev", 2);
}

/** <summary> Converts a Plain ClientRequest to an Encrypted Schema. </summary>

<param name="plain"> The Plain ClientRequest to convert. </param>
<param name="jsonMap"> Optional JSON Packet Map for encoding. </param>

<returns> An Encrypted Request Schema. </returns> */

public static ClientRequestEncryptedSchema FromPlain(ClientRequestSchema plain,
PacketCipher encryptor, JsonPacketMap jsonMap = null)
{
string type = plain.RequestType;
ClientRequestEncryptedSchema encrypted = new(type, plain.EncryptionVer);

dynamic data = plain.ClientData;

if(jsonMap is not null)
PacketParser.EncodeJsonNodes(data, jsonMap);

string jsonData = JsonSerializer.SerializeObject(data);
string crypto = encryptor(jsonData, type);

encrypted.ClientData = crypto;

return encrypted;
}

}

}