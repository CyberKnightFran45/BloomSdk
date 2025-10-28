using BlossomLib.Modules.Security;
using System;
using System.IO;

namespace NetworkMgr
{
/// <summary> Initializes Secure Functions for PopCap Binaries </summary>

public static class SecureBlob
{
/// <summary> The Key used. </summary>

private static readonly byte[] KEY = "12345678123456781234567812345678"u8.ToArray();

// Encrypt Raw Bytes

private static NativeString Encrypt(byte[] input) => Aes64.Encrypt(input, KEY);

// Encrypt String

public static string EncryptString(ReadOnlySpan<char> str)
{
var inputBytes = InputHelper.GetBytes(str);
using var encOwner = Encrypt(inputBytes);

return encOwner.ToString();
}

// Encrypt Stream

public static void EncryptStream(Stream input, Stream output)
{
using var iOwner = input.ReadPtr();
var inputBytes = iOwner.ToArray();

using var cOwner = Encrypt(inputBytes);

output.WriteString(cOwner.AsSpan() );
}

/** <summary> Encrypts Raw Bytes as a PopCapSecureBlob. </summary>

<param name = "inputPath"> The Path where the File to Encrypt is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param> */

public static void EncryptFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("SecureBlob Encryption Started");

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

TraceLogger.WriteLine("SecureBlob Encryption Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

// Decrypt Base64 String

private static byte[] Decrypt(ReadOnlySpan<char> input) => Aes64.Decrypt(input, KEY);

// Decode String

public static string DecryptString(ReadOnlySpan<char> str)
{
var rawBytes = Decrypt(str);

return InputHelper.GetString(rawBytes);
}

// Decrypt Stream

public static void DecryptStream(Stream input, Stream output)
{
using var iOwner = input.ReadString();
var rawBytes = Decrypt(iOwner.AsSpan() );

output.WriteBytes(rawBytes);
}

/** <summary> Decrypts a PopCapSecureBlob as Raw Bytes. </summary>

<param name = "inputPath"> The Path where the File to Decrypt is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param> */

public static void DecryptFile(string inputPath, string outputPath)
{
TraceLogger.Init();
TraceLogger.WriteLine("SecureBlob Decryption Started");

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

TraceLogger.WriteLine("SecureBlob Decryption Finished");

var outSize = FileManager.GetFileSize(outputPath);
TraceLogger.WriteInfo($"Output Size: {SizeT.FormatSize(outSize)}", false);
}

}

}