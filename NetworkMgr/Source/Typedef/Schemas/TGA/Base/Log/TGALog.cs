namespace NetworkMgr
{
/// <summary> Represents some Info sent to TGA Logs </summary>

public class TGALog : HttpUrlDoc<TGALog>
{
/** <summary> Gets or Sets the Request Key. </summary>
<returns> The Request Key. </returns> */

public string Key{ get; set; } = "";

/** <summary> Gets or Sets the Request Message. </summary>
<returns> The Request Message. </returns> */

public string Message{ get; set; } = "";

/// ctor

public TGALog()
{
Init();
}

/// ctor 2

public TGALog(string key, string msg)
{
Key = key;
Message = msg;

Init();
}

// Init Getters

protected override void InitGetters()
{
RegisterGetter(0, () => Key);
RegisterGetter(1, () => Message);
}

// Init Setters

protected override void InitSetters()
{
RegisterSetter(0, val => Key = val);
RegisterSetter(1, val => Message = val);
}

// Setup Fields

protected override void SetupFields()
{
RegisterField("key", 0);
RegisterField("message", 1);
}

// Serialize Info

public static TGALog FromRaw<T>(ILoggable<T> source) where T : class
{
string msg = source.Serialize();

return new(source.Key, msg);
}

}

}