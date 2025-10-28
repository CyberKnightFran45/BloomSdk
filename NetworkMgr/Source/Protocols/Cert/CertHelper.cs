using System;
using BlossomLib.Modules.Security;

namespace NetworkMgr
{
// Helper for Signing Certificates

public static class CertHelper
{
// Signs a String by using MD5 and a Salt

public static NativeMemoryOwner<char> Sign(ReadOnlySpan<char> str, ReadOnlySpan<byte> salt,
string hashType = "MD5")
{
using var mOwner = InputHelper.GetNativeBytes(str);

ulong bufferLen = mOwner.Size;
var saltLen = (ulong)salt.Length;

mOwner.Realloc(bufferLen + saltLen);
salt.CopyTo(mOwner.AsSpan(bufferLen) );

return GenericDigest.GetString(mOwner.AsSpan(), hashType);
}

}

}