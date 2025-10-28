using System.IO;

namespace SexyCalculator
{
/// <summary> Initializes calculating Functions between VarInt and Integer Values. </summary>

public static class VarInt
{
/** <summary> Calculates a VarInt from a given Integer Value. </summary>

<param name = "v"> The Integer where the VarInt will be Calculated from. </param>

<returns> The Varint Value Calculated. </returns> */

public static int ConvertTo(int v)
{
using BinaryStream buffer = new();

buffer.WriteVarInt(v);
buffer.PadStream(4);

buffer.Seek(0, SeekOrigin.Begin);

return buffer.ReadInt();
}

/** <summary> Calculates an Integer from a given VarInt Value. </summary>

<param name = "v"> The VarInt where the Integer will be Calculated from. </param>

<returns> The Integer Calculated. </returns> */

public static int ConvertFrom(int v)
{
using BinaryStream buffer = new();

buffer.WriteInt(v);
buffer.Seek(0, SeekOrigin.Begin);

return buffer.ReadVarInt();
}

}

}