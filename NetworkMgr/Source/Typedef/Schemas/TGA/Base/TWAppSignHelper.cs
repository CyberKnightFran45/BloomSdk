namespace NetworkMgr
{
/// <summary> Helper used for making a MD5 Signature from TW App. </summary>

public class TWAppSignHelper<T> : SignHelper<T> where T : class
{
/// Make Signature

protected override NativeMemoryOwner<char> Sign()
{
using var cOwner = GetContent();

return TWAppCert.Sign(cOwner.AsSpan() );
}

}

}