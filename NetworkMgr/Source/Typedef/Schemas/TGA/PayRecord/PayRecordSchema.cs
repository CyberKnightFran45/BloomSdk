using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a Schema that Recopiles Info about a PayRecord </summary>

public class PayRecordSchema
{
/** <summary> Gets or Sets the Operator ID. </summary>
<returns> The Operator ID. </returns> */

[JsonPropertyName("op") ]

public string OperatorID{ get; set; } = "200301";

/** <summary> Gets or Sets an ID for the Authorized Merchant. </summary>
<returns> The Merchant ID. </returns> */

[JsonPropertyName("merchantId") ]

public string MerchantID{ get; set; } = "010301";

/** <summary> Gets or Sets the App ID. </summary>
<returns> The App ID. </returns> */

[JsonPropertyName("applicationId") ]

public string AppID{ get; set; } = "109";

/** <summary> Gets or Sets the Version of the SDK used in <c>TWPay.xml</c> </summary>
<returns> The SDK Version </returns> */

[JsonPropertyName("sdkVersion") ]

public string SdkVersion{ get; set; } = "100001";

/** <summary> Gets or Sets the ICCID from User's Phone. </summary>
<returns> The ICCID </returns> */

[JsonPropertyName("iccid") ]

public string ICCID{ get; set; } = "";

/** <summary> Gets or Sets the Province from User's Phone. </summary>
<returns> The Province </returns> */

[JsonPropertyName("province") ]

public string Province{ get; set; } = "";

/** <summary> Gets or Sets the Operator Name from User's Phone. </summary>
<returns> The Operator Name </returns> */

[JsonPropertyName("operators") ]

public string Operator{ get; set; } = "";

/** <summary> Gets or Sets a Record of Pays made by User. </summary>
<returns> The Pay Record </returns> */

[JsonPropertyName("records") ]

public List<PayRecordInfo> PayRecord{ get; set; } = new();

/** <summary> Gets or Sets the IMEI from User's Phone. </summary>

<remarks> Must be 16-chars long, without Separators </remarks>

<returns> The IMEI </returns> */

[JsonPropertyName("imei") ]

public string IMEI{ get; set; } = new('0', 16);

/** <summary> Gets or Sets the IMSI from User's Phone. </summary>
<returns> The IMSI </returns> */

[JsonPropertyName("imsi") ]

public string IMSI{ get; set; } = "";

/** <summary> Gets or Sets the ID of App Channel. </summary>
<returns> The Channel ID. </returns> */

[JsonPropertyName("channelsId") ]

public string ChannelID{ get; set; } = "208";

/** <summary> Gets or Sets the MAC Address from User's Device (Optional). </summary>
<returns> The Mac Address </returns> */

[JsonPropertyName("mac") ]

public string Mac{ get; set; }

/** <summary> Gets or Sets a MD5 Signature from App used as Public Key. </summary>
<returns> The App Signature </returns> */

[JsonPropertyName("publicKey") ]

public string PublicKey{ get; set; } = "<appSignature>";

/** <summary> Gets or Sets the User Id. </summary>
<returns> The User Id </returns> */

[JsonPropertyName("idfa") ]

public string UserId{ get; set; } = "<userid>";

/** <summary> Gets or Sets the App Version. </summary>
<returns> The App Version </returns> */

[JsonPropertyName("appVersion") ]

public string AppVersion{ get; set; } = "1.0.0";

/** <summary> Gets or Sets the Package Name. </summary>
<returns> The Package Name </returns> */

[JsonPropertyName("package_name") ]

public string PackageName{ get; set; } = "com.popcap.pvz2cthd";

/// ctor

public PayRecordSchema()
{
}

/// Add Record to List

public void AddRecord(PayRecordInfo mRecord) => PayRecord.Add(mRecord);

/// Add new Record

public void AddNew() => AddRecord(new() );

/// Add Launch Record

public void AddLaunchRecord() => AddRecord(PayRecordInfo.Default);

/// Add Consume Record

public void AddConsumeRecord() => AddRecord(new("8888") );

/// Remove Record from List

public void DeleteRecord(int index)
{

if(PayRecord.Count > 0)
{
index = Math.Clamp(index, 0, PayRecord.Count - 1);

PayRecord.RemoveAt(index);
}

}

// Clear Record

public void ClearRecord() => PayRecord.Clear();

public static readonly JsonSerializerContext Context = new PayRecordContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(PayRecordInfo) ) ]

[JsonSerializable(typeof(PayRecordSchema) ) ]

public partial class PayRecordContext : JsonSerializerContext
{
}

}