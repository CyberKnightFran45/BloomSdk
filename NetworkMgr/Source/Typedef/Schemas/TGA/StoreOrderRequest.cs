using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a Request made by User when Buying something </summary>

public class StoreOrderRequest : HttpUrlDoc<StoreOrderRequest>
{
/** <summary> Gets or Sets the Chinese National Identity Card number. </summary>

<remarks> Must be exactly 18-chars long (GB 11643-1999 standard, no separators): <para>

</para> <c>- First 6 digits: Administrative region code.</c> <para>
</para> <c>- Next 8 digits: Date of birth (YYYYMMDD).</c> <para>
</para> <c>- Next 3 digits: Personal sequence code (odd = male, even = female). </c> </remarks>

<returns> The Chinese ID card. </returns> */

[JsonPropertyName("identityCard") ]

public string IdCard{ get; set; } = "330921199303050039";

/** <summary> Gets or Sets the User Id. </summary>
<returns> The User Id </returns> */

[JsonPropertyName("userId") ]

public string UserId{ get; set; } = "";

/** <summary> Gets or Sets the Amount of Money spent. </summary>
<returns> The Money spent. </returns> */

[JsonPropertyName("money") ]

public string MoneyPayed{ get; set; } = "0";

/// ctor

public StoreOrderRequest()
{
Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => IdCard);
RegisterGetter(1, () => UserId);
RegisterGetter(2, () => MoneyPayed);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => IdCard = val);
RegisterSetter(1, val => UserId = val);
RegisterSetter(2, val => MoneyPayed = val);
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("identityCard", 0);
RegisterField("userId", 1);
RegisterField("money", 2);
}

public static readonly JsonSerializerContext Context = new OrderContext(JsonSerializer.Options);
}

// Context for serialization

[JsonSerializable(typeof(StoreOrderRequest) ) ]

public partial class OrderContext : JsonSerializerContext
{
}

}