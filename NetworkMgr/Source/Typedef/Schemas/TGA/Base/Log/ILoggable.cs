using System;
using System.Text.Json.Serialization;

namespace NetworkMgr
{
/// <summary> Represents a Interface that Allows Logging Info to TGA </summary>

public abstract class ILoggable<T> : FieldMgr<T> where T : class
{
/** <summary> Gets or Sets a Key that identifies the Log Type. </summary>
<returns> The Log Key. </returns> */

[JsonPropertyName("key") ]

public string Key{ get; set; } = "";

// Append new Field

private static void AppendField(ReadOnlySpan<char> source, Span<char> target, ref int pos)
{
source.CopyTo(target[pos..]);

pos += source.Length;
target[pos++] = '|';
}

// Serialize Fields

public string Serialize()
{
using NativeString strOwner = new(512);
var buffer = strOwner.AsSpan();

int maxIndex = _fieldSetters.Count;
int pos = 0;

for(int i = 0; i <= maxIndex; i++)
{

if(_fieldGetters.TryGetValue(i, out var getter) )
{
string val = getter();

AppendField(val, buffer, ref pos);
}

}

if(pos > 0 && buffer[pos - 1] == '|')
pos--;

strOwner.Realloc(pos);

return strOwner.ToString();
}

// Deserialize fields from String

private void DeserializeFields(ReadOnlySpan<char> msg)
{
int fieldIndex = 0;
int start = 0;

int maxIndex = _fieldSetters.Count;

while(fieldIndex <= maxIndex && start < msg.Length)
{
int next = msg[start..].IndexOf('|');
ReadOnlySpan<char> segment;

if(next == -1)
{
segment = msg[start..];
start = msg.Length;
}

else
{
segment = msg.Slice(start, next);
start += next + 1;
}

if(_fieldSetters.TryGetValue(fieldIndex, out var setter) )
setter(segment.ToString() );

fieldIndex++;
}

}

// Deserialize Log

public void Deserialize(TGALog source) => DeserializeFields(source.Message);

// Check null Fields

public abstract void CheckFields();
}

}