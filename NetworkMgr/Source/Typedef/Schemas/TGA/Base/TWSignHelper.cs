namespace NetworkMgr
{
/// <summary> Helper used for making a MD5 Signature from inherited Class fields. </summary>

public class TWSignHelper<T> : SignHelper<T> where T : class
{
/// Make Signature

protected override NativeMemoryOwner<char> Sign()
{
using var cOwner = GetContent();

return TWCert.Sign(cOwner.AsSpan() );
}

}

}