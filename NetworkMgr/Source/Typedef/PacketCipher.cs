using System;

namespace NetworkMgr
{
// Cipher delegate

public delegate string PacketCipher(ReadOnlySpan<char> data, ReadOnlySpan<char> type);
}