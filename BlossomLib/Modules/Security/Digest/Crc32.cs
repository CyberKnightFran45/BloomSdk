using SharpCrc32 = ICSharpCode.SharpZipLib.Checksum.Crc32;

using System;
using System.IO;

namespace BlossomLib.Modules.Security
{
/// <summary> Initializes Crc32 Checksum Tasks for Byte Arrays and Streams. </summary>

public static class Crc32
{
/** <summary> Gets the Checksum of an Array of Bytes by using the Crc32 Algorithm. </summary>

<param name = "data"> The Bytes where the Checksum will be Obtained from. </param>

<returns> The Crc32 Checksum. </returns> */

public static long Calculate(byte[] data)
{
SharpCrc32 crc32 = new();
crc32.Update(data);

long checksum = crc32.Value;
crc32.Reset();

return checksum;
}

/** <summary> Gets the Checksum of a Stream by using the Crc32 Algorithm. </summary>

<param name = "input"> The Stream where the Checksum will be Obtained from. </param>

<returns> The Adler32 Checksum. </returns> */

public static long Calculate(Stream input)
{
int blockSize = MemoryManager.GetBlockSize(input);

using NativeMemoryOwner<byte> bOwner = new(blockSize);
var buffer = bOwner.ToArray();

SharpCrc32 crc32 = new();
int bytesRead;

while( (bytesRead = input.Read(buffer) ) > 0)
{
ArraySegment<byte> segment = new(buffer, 0, bytesRead);
crc32.Update(segment);
}

long checksum = crc32.Value;
crc32.Reset();

return checksum;
}

}

}