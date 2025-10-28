using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

/// <summary> Initializes Filtering Functions for Input Values. </summary>

public static partial class InputHelper
{
/// <summary> Searches for a Number on a String. </summary>

[GeneratedRegex("([-+]?\\d*\\.?\\d+)")]

private static partial Regex NumericPattern();

/// <summary> Applies a String Case to a Span of Characters. </summary>

public static void ApplyStringCase(Span<char> target, StringCase strCase)
{

switch (strCase)
{
case StringCase.Lower:

for (int i = 0; i < target.Length; i++)
target[i] = char.ToLowerInvariant(target[i]);

break;

case StringCase.Upper:

for (int i = 0; i < target.Length; i++)
target[i] = char.ToUpperInvariant(target[i]);

break;
}

}

/// <summary> Converts a Hexadecimal Character to its Byte Representation. </summary>
/// <param name="c"> The HexChar to Convert. </param>

private static byte ConvertHexChar(char c)
{

if (c >= '0' && c <= '9') return (byte)(c - '0');
if (c >= 'A' && c <= 'F') return (byte)(c - 'A' + 10);
if (c >= 'a' && c <= 'f') return (byte)(c - 'a' + 10);

return 0;
}

/// <summary> Converts a Hexadecimal String to a Sequence of Bytes. </summary>
/// <param name="hexStr"> The Hexadecimal String to Convert. </param>
/// <param name="separator"> The Separator Character to use between Hex Bytes. </param>
/// <returns> A Native Memory Owner containing the Converted Bytes. </returns>

public static NativeMemoryOwner<byte> ConvertHexBytes(ReadOnlySpan<char> hexStr, char separator = ' ')
{
	
if(hexStr.IsEmpty)
return new();

int maxBytes = (hexStr.Length + 1) / 2;

NativeMemoryOwner<byte> memOwner = new(maxBytes);
var span = memOwner.AsSpan();

int byteCount = 0;
int i = 0;

while (i < hexStr.Length)
{

while(i < hexStr.Length && hexStr[i] == separator)
i++;

if(i + 1 >= hexStr.Length)
break;

byte high = ConvertHexChar(hexStr[i]);
byte low = ConvertHexChar(hexStr[i + 1]);

span[byteCount++] = (byte)((high << 4) | low);
i += 2;
}

memOwner.Realloc((ulong)byteCount);

return memOwner;
}

/// <summary> Converts a Hexadecimal Value to its Character Representation. </summary>
/// <param name="v"> The Hexadecimal Value to Convert. </param>
/// <param name="strCase"> The String Case to apply to the Character. </param>
/// <returns> The Character Representation of the Hexadecimal Value. </returns>

private static char ToHexChar(int v, StringCase strCase)
{
char offset = strCase == StringCase.Upper ? 'A' : 'a';

return (char)(v < 10 ? '0' + v : offset + (v - 10));
}

/** <summary> Converts an Array of Bytes into a Hexadecimal String. </summary>

<param name = "input"> The Bytes to Convert. </param>
<param name = "strCase"> The String Case to apply to the Hex Characters. </param>
<param name = "separator"> The Separator Character to use between Hex Bytes. </param>

<returns> The HexString Converted. </returns> */

public static NativeMemoryOwner<char> ConvertHexString(ReadOnlySpan<byte> input, StringCase strCase,
char separator = '\0')
{

if(input.IsEmpty)
return new();

int maxLen = input.Length * 2 + (separator == '\0' ? 0 : (input.Length - 1));
NativeMemoryOwner<char> memOwner = new(maxLen);

var span = memOwner.AsSpan();
int index = 0;

for(int i = 0; i < input.Length; i++)
{
byte b = input[i];

span[index++] = ToHexChar(b >> 4, strCase);
span[index++] = ToHexChar(b & 0xF, strCase);

if (i < input.Length - 1 && separator != '\0')
span[index++] = separator;

}

memOwner.Realloc( (ulong)index);

return memOwner;
}

/** <summary> Filters a <c>DateTime</c> from user's Input. </summary>

<returns> The Filtered Value. </returns> */
 
public static DateTime FilterDateTime(ReadOnlySpan<char> input)
{
bool isValidDate = DateTime.TryParse(input, LibInfo.CurrentCulture, out DateTime filteredDate);

return isValidDate ? filteredDate : DateTime.Now;
}

/** <summary> Filters a Name from User's Input. </summary>

<param name = "source"> The Name to be Filtered. </param>

<returns> The Filtered Name. </returns> */
 
public static NativeMemoryOwner<char> FilterName(ReadOnlySpan<char> source)
{
NativeMemoryOwner<char> buffer = new( (ulong)source.Length);
var span = buffer.AsSpan();

int count = 0;
var invalidChars = GetInvalidChars(true);

foreach (char c in source)
{

if(Array.IndexOf(invalidChars, c) == -1)
span[count++] = c;

}

buffer.Realloc( (ulong)count);

return buffer;
}

/** <summary> Extracts the Numeric Digits from user's Input. </summary>

<param name = "sourceStr"> The String to be Analized. </param>

<returns> A Sequence of Chars that represent the numerical Digits. </returns> */

private static NativeMemoryOwner<char> ExtractNumericDigits(ReadOnlySpan<char> sourceStr)
{
var numericMatch = NumericPattern().Match(sourceStr.ToString());

if(numericMatch.Success)
{
Group numbers = numericMatch.Groups[1];
var digits = numbers.Value.AsSpan();

NativeMemoryOwner<char> owner = new(digits.Length);
var span = owner.AsSpan();

digits.CopyTo(span);

return owner;
}

NativeMemoryOwner<char> zOwner = new(1);
zOwner.Fill('0');

return zOwner;
}

/** <summary> Filters a numeric Value from user's Input. </summary>

<param name = "input"> Input to be Filtered. </param>

<returns> The Filtered Value. </returns> */

public static T FilterNumber<T>(ReadOnlySpan<char> input) where T : struct
{
using var digits = ExtractNumericDigits(input);

return ValidateNumericRange<T>(digits.AsSpan() );
}

// Generate Random String

public static string GenRandomStr(int length)
{
const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

return RandomNumberGenerator.GetString(ALPHABET, length);
}

/** <summary> Gets a List of Invalid Chars for FileNames or DirNames. </summary>

<param name = "isShortName"> Determines if the File/Folder name is a Name (Short Name) 
or a Path (Full Name). </param>

<returns> The Invalid Chars. </returns> */

public static char[] GetInvalidChars(bool isShortName)
{
return isShortName ? Path.GetInvalidFileNameChars() : Path.GetInvalidPathChars();
}

/** <summary> Converts a Sequence of Bytes into a String. </summary>

<param name = "bytes"> The Bytes to Convert. </param>
<param name = "encoding"> The Encoding to be used for Conversion. </param>

<returns> The String Representation of the Bytes. </returns> */

public static string GetString(ReadOnlySpan<byte> bytes, Encoding encoding = null)
{
using var strOwner = GetNativeString(bytes, encoding);

Span<char> strSpan = strOwner.AsSpan();

return new(strSpan);
}

/** <summary> Converts a Sequence of Bytes into a Native String. </summary>

<param name = "bytes"> The Bytes to Convert. </param>
<param name = "encoding"> The Encoding to be used for Conversion. </param>

<returns> The Native String. </returns> */

public static NativeString GetNativeString(ReadOnlySpan<byte> bytes, Encoding encoding = null)
{
encoding ??= Encoding.UTF8;

int charCount = encoding.GetCharCount(bytes);
NativeString str = new(charCount);

var charSpan = str.AsSpan();
encoding.GetChars(bytes, charSpan);

return str;
}

public static byte[] GetBytes(ReadOnlySpan<char> str, Encoding encoding = null)
{
using NativeMemoryOwner<byte> bytes = GetNativeBytes(str, encoding);

return bytes.ToArray();
}

/** <summary> Converts a Sequence of Characters into a Native Byte Array. </summary>

<param name = "str"> The Characters to Convert. </param>
<param name = "encoding"> The Encoding to be used for Conversion. </param>

<returns> The Native Byte Array. </returns> */

public static NativeMemoryOwner<byte> GetNativeBytes(ReadOnlySpan<char> str, Encoding encoding = null)
{
encoding ??= Encoding.UTF8;

if(str.IsEmpty)
return new(0);

int byteCount = encoding.GetByteCount(str);
NativeMemoryOwner<byte> bytes = new(byteCount);

var byteSpan = bytes.AsSpan();
encoding.GetBytes(str, byteSpan);

return bytes;
}

/** <summary> Removes Literal Characters from a String. </summary>

<remarks> This Method is used to remove Chars such as \r, \n, \t </remarks>

<param name = "targetStr"> The String to be Processed. </param> */  

public static void RemoveLiteralChars(ref string targetStr)
{
targetStr = targetStr.Replace("\\r\\n", string.Empty)
.Replace("\\r", string.Empty)
.Replace("\\n", string.Empty)
.Replace("\\t", string.Empty);
}

/** <summary> Checks if a sequence of Numbers represented as a String is on Range or not. </summary>

<typeparam name = "T"> The Type of the Numeric Value to be Validated. </typeparam>
<param name = "numericDigits"> The numeric Digits Sequence. </param>

<returns> A Value that is Inside the Range of the expected Type. </returns> */

private static T ValidateNumericRange<T>(ReadOnlySpan<char> numericDigits) where T : struct
{
object parsedObj = Convert.ChangeType(numericDigits.ToString(), typeof(T));
T numericValue = (parsedObj == null) ? default : (T)parsedObj;

return numericValue;
}

}