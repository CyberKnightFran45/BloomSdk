using System;

namespace SexyCalculator
{
/// <summary> Initializes Bitwise Operations between Integers and their Bin/Hex representation. </summary>

public static class BinaryConverter
{
// And Operation

public static int And(int a, int b) => a & b;

// Or Operation

public static int Or(int a, int b) => a | b;

// Xor Operation

public static int Xor(int a, int b) => a ^ b;

// Nor Operation (Complement)

public static int Nor(int a) => ~a;

// Shift Bits (Left or Right)

public static int Shift(int a, int b, bool toRight) => toRight ? a >> b : a << b;

// Convert Int to Hex

public static string ToHex(int v) => v.ToString("X");

// Convert Int to Bin

public static string ToBin(int v) => Convert.ToString(v, 2);

// Convert from Hex to Int

public static int FromHex(string v) => Convert.ToInt32(v, 16);

// Convert from Bin to Int

public static int FromBin(string v) => Convert.ToInt32(v, 2);
}

}