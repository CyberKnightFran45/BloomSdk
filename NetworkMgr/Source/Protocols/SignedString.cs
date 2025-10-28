using BlossomLib.Modules.Security;
using System;
using System.IO;

namespace NetworkMgr
{
/// <summary> Encrypts Data with AES-GCM and Signs it with SHA-256 </summary>

public static class SignedString
{
/// <summary> The Key used. </summary>

private static readonly byte[] KEY = GetKey();

/// <summary> A Seed used for Making Digest. </summary>

private static readonly byte[] RANDOM_SEED = GetSeed();

// Get Cipher Key

private static byte[] GetKey()
{
using var rOwner = InputHelper.ConvertHexBytes("2836e95fcd10e04b0069bb1ee659955b", '\0');

return rOwner.ToArray();
}

// Get Seed

private static byte[] GetSeed()
{
string s = "appIdtest-appIdbizIdtest-bizIdidtest-idnametest-nametimestamps1584949895758";

return InputHelper.GetBytes(s);
}

// Sign bytes with SHA-256 by using a Salt with a Seed

private static NativeMemoryOwner<char> Sign(ReadOnlySpan<byte> input) 
{
var keyLen = (ulong)KEY.Length;
var seedLen = (ulong)RANDOM_SEED.Length;

ulong prefixLen = keyLen + seedLen;
var totaLen = prefixLen + (ulong)input.Length;

using NativeMemoryOwner<byte> mOwner = new(totaLen);

KEY.CopyTo(mOwner.AsSpan() );
RANDOM_SEED.CopyTo(mOwner.AsSpan(keyLen) );
input.CopyTo(mOwner.AsSpan(prefixLen) );

return GenericDigest.GetString(mOwner.AsSpan(), "SHA256");
}

// Sign chars

private static NativeMemoryOwner<char> Sign(ReadOnlySpan<char> input)
{
using var rOwner = InputHelper.GetNativeBytes(input);

return Sign(rOwner.AsSpan() );
}

// Hash string

public static string GetHash(ReadOnlySpan<char> input) 
{
using var sOwner = Sign(input);

return sOwner.ToString();
}

// Encrypt Raw Bytes

private static NativeString EncryptBytes(ReadOnlySpan<byte> input)
{
var encOwner = AesGcm64.Encrypt(input, KEY);
long bufferLen = encOwner.Length;

using var sOwner = Sign(encOwner.AsSpan() );
var hashLen = (long)sOwner.Size;

long totaLen = bufferLen + hashLen + 2;
encOwner.Realloc(totaLen);

var hashIndex = (ulong)bufferLen;

"\n\n".CopyTo(encOwner.AsSpan(hashIndex) );
sOwner.AsSpan().CopyTo(encOwner.AsSpan(hashIndex + 2) );

return encOwner;
}

// Encrypt String

public static string Encrypt(ReadOnlySpan<char> str)
{
using var rOwner = InputHelper.GetNativeBytes(str);
using var encOwner = EncryptBytes(rOwner.AsSpan() );

return encOwner.ToString();
}

// Encrypt Stream

public static void EncryptStream(Stream input, Stream output)
{
using var iOwner = input.ReadPtr();
using var cOwner = EncryptBytes(iOwner.AsSpan() );

output.WriteString(cOwner.AsSpan() );
}

/** <summary> Encrypt and Sign Raw Bytes. </summary>

<param name = "inputPath"> The Path where the File to Encrypt is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param> */

public static void EncryptFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("SignedString Encryption Started");

try
{
TraceLogger.WriteDebug($"{inputPath} → {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

string fileSize = SizeT.FormatSize(inFile.Length);

TraceLogger.WriteActionStart($"Encrypting data... ({fileSize})");
EncryptStream(inFile, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Encrypt file");
}

TraceLogger.WriteLine("SignedString Encryption Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Decrypt Signed String

private static NativeMemoryOwner<byte> DecryptBytes(ReadOnlySpan<char> input)
{
int hashIndex = input.IndexOf("\n\n");

if(hashIndex != -1)
input = input[..hashIndex]; // Ignore SHA-256 digest

return AesGcm64.Decrypt(input, KEY);
}

// Decode String

public static string Decrypt(ReadOnlySpan<char> str)
{
using var rOwner = DecryptBytes(str);

return InputHelper.GetString(rOwner.AsSpan() );
}

// Decrypt Stream

public static void DecryptStream(Stream input, Stream output)
{
using var iOwner = input.ReadString();
using var rOwner = DecryptBytes(iOwner.AsSpan() );

output.WriteBytes(rOwner.AsSpan() );
}

/** <summary> Decrypts SignedString as Raw Bytes. </summary>

<param name = "inputPath"> The Path where the File to Decrypt is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param> */

public static void DecryptFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("SignedString Decryption Started");

try
{
TraceLogger.WriteDebug($"{inputPath} → {outputPath}");

TraceLogger.WriteActionStart("Opening files...");

using FileStream inFile = FileManager.OpenRead(inputPath);
using FileStream outFile = FileManager.OpenWrite(outputPath);

TraceLogger.WriteActionEnd();

string fileSize = SizeT.FormatSize(inFile.Length);

TraceLogger.WriteActionStart($"Decrypting data... ({fileSize})");
DecryptStream(inFile, outFile);

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError(error, "Failed to Decrypt file");
}

TraceLogger.WriteLine("SignedString Decryption Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

}

}