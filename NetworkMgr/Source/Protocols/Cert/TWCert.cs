using System;

namespace NetworkMgr
{
// Sign Strings by using a TalkWeb Cert

public static class TWCert
{
// The Salt used

private static readonly byte[] SALT = "talkwebCert"u8.ToArray();

// Sign string with MD5 by using a SaltValue

public static NativeMemoryOwner<char> Sign(ReadOnlySpan<char> str) => CertHelper.Sign(str, SALT);
}

}