using System;
using System.Runtime.InteropServices;

/** <summary> Represents a memory owner for unmanaged types using native memory allocation. </summary>

<typeparam name="T">The unmanaged type of the memory owner.</typeparam> */

public unsafe struct NativeMemoryOwner<T> : IDisposable where T : unmanaged
{
/// <summary> Pointer to the allocated memory. </summary>

private T* _ptr;

/// <summary> Size of the allocated memory in elements of type T. </summary>

private ulong _size;

/// <summary> Indicates whether the memory has been disposed. </summary>

private bool _disposed;

/// <summary> Gets the size of the allocated memory. </summary>

public readonly ulong Size => _size;

/** <summary> Initializes a new instance of the <see cref="NativeMemoryOwner{T}"/> struct. </summary>

<remarks> The memory is allocated with zero size. </remarks> */

public NativeMemoryOwner()
{
_ptr = null;
}

/** <summary> Initializes a new instance of the <see cref="NativeMemoryOwner{T}"/> 
struct with a specified length. </summary>

<param name="len">The length of the memory to allocate in elements of type T.</param> */

public NativeMemoryOwner(ulong len)
{
_size = CapToMaxAllocatable(len);
_ptr = (T*)NativeMemory.Alloc( (nuint)(_size * (ulong)sizeof(T) ) );
}

/// <summary> Initializes a new instance of the <see cref="NativeMemoryOwner{T}"/> 
/// struct with a specified length. </summary>
/// <param name="len">The length of the memory to allocate in bytes.</param>

public NativeMemoryOwner(long len) : this(len < 0 ? 0 : (ulong)len)
{
}

// Get max allocatable size based on the platform's pointer size.

private static ulong CapToMaxAllocatable(ulong size)
{
ulong maxAlloc = sizeof(nuint) == 4 ? uint.MaxValue : ulong.MaxValue;
ulong maxCount = maxAlloc / (ulong)sizeof(T);

return size > maxCount ? maxCount : size;
}

/// <summary>
/// Creates a span over the allocated memory starting from the specified offset.
/// If the offset is greater than or equal to the size, an empty span is returned.
/// </summary>
/// <param name="offset"></param>
/// <returns></returns>

public readonly Span<T> AsSpan(ulong offset = 0, int length = -1)
{
    if (_ptr == null || _size == 0 || offset >= _size)
        return [];

    ulong maxLength = _size - offset;

    if (length < 0 || (ulong)length > maxLength)
        length = (int)Math.Min(maxLength, int.MaxValue);

    return new(_ptr + offset, length);
}


// In place copy

public readonly void Move(ulong sourceOffset, ulong destinationOffset, ulong count)
{
    if (_disposed || _ptr == null || _size == 0 || count == 0)
        return;

    if (sourceOffset >= _size || destinationOffset >= _size)
        return;

    ulong maxCount = _size - Math.Max(sourceOffset, destinationOffset);
    if (count > maxCount)
        count = maxCount;

    void* source = _ptr + sourceOffset;
    void* destination = _ptr + destinationOffset;
    ulong bytes = count * (ulong)sizeof(T);

    Buffer.MemoryCopy(source, destination, bytes, bytes);
}

/// <summary> Converts the allocated memory to an Array of type T.
/// If the size is zero, an empty array is returned.
/// </summary>

public readonly T[] ToArray(ulong offset = 0, int length = -1)
{
Span<T> view = AsSpan(offset, length);
var array = new T[view.Length];

view.CopyTo(array);

return array;
}

/// <summary> Fills the Memory with bytes. </summary>

public readonly void Fill(T v)
{

if (_disposed || _ptr == null || _size == 0)
	return;

for(ulong i = 0; i < _size; i++)
_ptr[i] = v;

}

/// <summary> Clears the allocated memory by setting all bytes to zero. </summary>

public readonly void Clear()
{

if (_disposed || _ptr == null || _size == 0)
return;

NativeMemory.Clear(_ptr, (nuint)(_size * (ulong)sizeof(T) ) );
}

/** <summary> Reallocates the memory to a new size. </summary>

<remarks> If the pointer is null, it allocates new memory. </remarks>

<param name="n">The new size in elements of type T.</param> */

public void Realloc(ulong n)
{
var maxAlloc = CapToMaxAllocatable(n);
var sizeInBytes = (nuint)(maxAlloc * (ulong)sizeof(T) );

if (_ptr == null)
_ptr = (T*)NativeMemory.Alloc(sizeInBytes);

else
_ptr = (T*)NativeMemory.Realloc(_ptr, sizeInBytes);

_size = maxAlloc;
}

/// <summary> Disposes the memory owner, releasing the allocated memory 
/// if it has not been disposed yet. </summary>

public void Dispose()
{

if(!_disposed)
{

if(_ptr != null)
{
NativeMemory.Free(_ptr);

_ptr = null;
_size = 0;
}

_disposed = true;
}

}

}