namespace NetworkMgr
{
/// <summary> Represents a Context where Http is used for Sending or Receiving data </summary>

public enum HttpContext
{
/// <summary> Generic data is sent in online events. </summary>
Generic,

/// <summary> <c>RechargeCheck</c> sent when Buying something. </summary>
RechargeCheck,

/// <summary> <c>BuryingPoint</c> sent to TalkWeb Servers. </summary>
BuryingPoint,

/// <summary> <c>Chinese Citizen ID Card</c> for Real-name Auth. </summary>
IdCard,

/// <summary> <c>UserInfo</c> from TalkWeb Account. </summary>
UserInfo,

/// <summary> Represents an <c>Order</c> made in Store. </summary>
Order,

/// <summary> Represents a <c>PayRecord</c> from User. </summary>
PayRecord,

/// <summary> <c>Login</c> Request sent to TalkWeb. </summary>
Login,

/// <summary> A log containing Info about User's Device. </summary>
UserTerminal
}

}