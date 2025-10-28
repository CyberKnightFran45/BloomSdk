using System;

namespace NetworkMgr
{
// Certificate for TW App

public static class TWAppCert
{
// The Salt used, obtained from Apk Assets: twOfflinePay.xml (may change between App versions)

private static readonly byte[] SALT = "9f6adc2f-00ba-4ec1-a3fa-e7196e3eaccf"u8.ToArray();

// Sign string with MD5 by using a SaltValue

public static NativeMemoryOwner<char> Sign(ReadOnlySpan<char> str) => CertHelper.Sign(str, SALT);
}

}