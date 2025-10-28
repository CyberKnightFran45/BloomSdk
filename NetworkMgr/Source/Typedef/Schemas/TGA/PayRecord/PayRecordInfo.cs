using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Info for PayRecordSchema </summary>

public class PayRecordInfo
{
/** <summary> Gets or Sets a Identifier that describes the Pay Type. </summary>
<returns> The Pay Type. </returns> */

[JsonPropertyName("payTypeId") ]

public string PayType{ get; set; } = "0";

/** <summary> Gets or Sets a Identifier that describes the Goods bought. </summary>
<returns> The Goods ID. </returns> */

[JsonPropertyName("goodsId") ]

public string GoodsID{ get; set; } = "0";

/** <summary> Gets or Sets the Amount of Goods bought. </summary>
<returns> The Goods Number. </returns> */

[JsonPropertyName("goodsNum") ]

public string GoodsNumber{ get; set; } = "0";

/** <summary> Gets or Sets the Goods Price. </summary>
<returns> The Goods Price. </returns> */

[JsonPropertyName("money") ]

public string GoodsPrice{ get; set; } = "0";

/** <summary> Gets or Sets the Currency	Unit. </summary>
<returns> The Currency Unit. </returns> */

[JsonPropertyName("currencyUnit") ]

public string CurrencyUnit{ get; set; } = "0";

/** <summary> Gets or Sets the Order Number. </summary>
<returns> The Order Number. </returns> */

[JsonPropertyName("orderNumber") ]

public string OrderNumber{ get; set; } = "0";

/** <summary> Gets or Sets a String that Contains the Time when the Pay was made. </summary>
<returns> The Request Time. </returns> */

[JsonPropertyName("payRequestTime") ]

public string RequestTime{ get; set; }

/** <summary> Gets or Sets a String that Contains the Time when the Pay was made. </summary>
<returns> The Request Time. </returns> */

[JsonPropertyName("payStat") ]

public string PayState{ get; set; } = "9999";

/** <summary> Gets or Sets the Amount of Money spent (Optional). </summary>
<returns> The Money spent. </returns> */

[JsonPropertyName("payMoney") ]

public string PayMoney{ get; set; }

// Default Info

public static readonly PayRecordInfo Default = new();

// Inner ctor

private PayRecordInfo()
{
RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}

// ctor 2

public PayRecordInfo(string state = "")
{
PayState = string.IsNullOrWhiteSpace(state) ? "9999" : state;
GoodsNumber = "1";

RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}

public static readonly JsonSerializerContext Context = new PayRecordSubContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(PayRecordInfo) ) ]

public partial class PayRecordSubContext : JsonSerializerContext
{
}

}