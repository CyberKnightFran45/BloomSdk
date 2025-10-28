namespace NetworkMgr
{
/// <summary> Represents the Different String Types a CN Json can have </summary>

public enum JsonStrFlags
{
/// <summary> Json Str is Empty </summary>
EmptyString,

/// <summary> Json Str is WhiteSpace </summary>
WhiteSpace,

/// <summary> Json Str is PlainText </summary>
PlainText,

/// <summary> Json Str is a Number </summary>
Number,

/// <summary> Json Str is a DateTime </summary>
DateTime,

/// <summary> Json Str is Base64 </summary>
Base64,

/// <summary> Json Str is Base64 (Web-Safe) </summary>
Base64Url,

/// <summary> Json Str is a Raw Payload (uses Base64 + GZip) </summary>
RawPayload,

/// <summary> Json Str is a Child Array </summary>
JsonArray,

/// <summary> Json Str is a Child Node </summary>
JsonObject,
}

}