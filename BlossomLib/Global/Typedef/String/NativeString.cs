// Encapsulates a CharPtr and Allows casting it to String

using System;

public struct NativeString : IDisposable
{
private NativeMemoryOwner<char> _buffer;

public NativeString(long length)
{
_buffer = new(length);
}

public NativeString(ulong length)
{
_buffer = new(length);
}

public readonly Span<char> AsSpan(ulong offset = 0, int length = -1) => _buffer.AsSpan(offset, length);

public readonly ReadOnlySpan<char> GetView(ulong offset, int length) => AsSpan(offset, length);

public readonly long Length => (long)_buffer.Size;

public void Dispose() => _buffer.Dispose();

public void Realloc(long n) => _buffer.Realloc( (ulong)n);

public readonly string Substring(ulong offset, int length)
{
var strView = GetView(offset, length);

return new(strView);
}

public override readonly string ToString() => Substring(0, -1);
}