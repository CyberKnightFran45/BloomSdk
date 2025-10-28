namespace SexyCalculator
{
/// <summary> Initializes ciphering Functions for Integers. </summary>

public static class IntGuard
{
/// <summary> The Key used for Offuscating Values. </summary>

private const int KEY = 13;

/// <summary> A Factor used for Locking bits. </summary>

private const int FACTOR = 0x1F;

/// <summary> The base an Integer must have. </summary>

private const int BASE = 32;

/// <summary> A Mask that avoids bit overflows. </summary>

private const int MASK = 0xFF;

/** <summary> Encrypts an Integer value by Performing some bitwise Operations on it. </summary>

<param name = "v"> The Value to Encrypt. </param>

<returns> The Encrypted Value. </returns> */

public static int Encrypt(uint v)
{
uint x = v ^ KEY;

int shiftL = KEY & FACTOR;
int shiftR = (BASE - shiftL) & MASK;
	
return (int)((x << shiftL) | (x >> shiftR));
}

/** <summary> Decrypts an Integer value by Performing some bitwise Operations on it. </summary>

<param name = "v"> The Value to Decrypt. </param>

<returns> The Decrypted Value. </returns> */

public static int Decrypt(uint v)
{
int shiftR = KEY & FACTOR;
int shiftL = (BASE - shiftR) & MASK;

uint x = (v >> shiftR) | (v << shiftL);

return (int)(x ^ KEY);
}

}

}