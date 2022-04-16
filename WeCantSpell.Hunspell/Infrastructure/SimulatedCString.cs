using System;
using System.Threading;

namespace WeCantSpell.Hunspell.Infrastructure;

struct SimulatedCString
{
    private const int DefaultCacheCapacity = 64;
    private const int MaxCacheCapacity = DefaultCacheCapacity * 2;
    private static char[]? BufferCache = new char[DefaultCacheCapacity];

    private static char[] GetBuffer(int minCapacity)
    {
        var result = Interlocked.Exchange(ref BufferCache, null);
        if (!(result?.Length >= minCapacity))
        {
            result = new char[Math.Max(minCapacity, DefaultCacheCapacity)];
        }

        return result;
    }

    private static void ReturnBuffer(char[] buffer)
    {
        if (buffer.Length != 0 && buffer.Length < MaxCacheCapacity)
        {
            Volatile.Write(ref BufferCache, buffer);
        }
    }

    public SimulatedCString(int capacity)
    {
        if (capacity <= 0)
        {
            capacity = 1;
        }

        _rawBuffer = GetBuffer(capacity);
        _bufferLength = capacity;
        _rawBuffer[0] = '\0';
        _terminatedLength = 0;
    }

    public SimulatedCString(ReadOnlySpan<char> text)
    {
        _rawBuffer = GetBuffer(text.Length + 3); // 3 extra characters seems to be enough to prevent most reallocations
        text.CopyTo(_rawBuffer.AsSpan(0, text.Length));
        _bufferLength = text.Length;
        _terminatedLength = -1;
    }

    private char[] _rawBuffer;
    private int _bufferLength;
    private int _terminatedLength;

    public int BufferLength => _bufferLength;

    public ReadOnlySpan<char> TerminatedSpan
    {
        get
        {
            if (_terminatedLength < 0)
            {
                _terminatedLength = Array.IndexOf(_rawBuffer, '\0', 0, _bufferLength);
                if (_terminatedLength < 0)
                {
                    _terminatedLength = _bufferLength;
                }
            }

            return _rawBuffer.AsSpan(0, _terminatedLength);
        }
    }

    public char this[int index]
    {
        get
        {
            return index >= 0 && index < _bufferLength ? _rawBuffer[index] : '\0';
        }
        set
        {
#if DEBUG
            if (index < 0 || index >= _bufferLength) throw new ArgumentOutOfRangeException(nameof(index));
#endif
            _rawBuffer[index] = value;

            if (value == '\0')
            {
                if (index < _terminatedLength)
                {
                    _terminatedLength = index;
                }
            }
            else if (index == _terminatedLength)
            {
                _terminatedLength = -1;
            }
        }
    }

    public ReadOnlySpan<char> SliceToTerminator(int startIndex)
    {
        if (startIndex <= _terminatedLength)
        {
            return _rawBuffer.AsSpan(startIndex, _terminatedLength - startIndex);
        }

        var result = _rawBuffer.AsSpan(startIndex, _bufferLength - startIndex);
        var index = result.IndexOf('\0');
        if (index >= 0)
        {
            result = result.Slice(0, index);
        }

        return result;
    }

    public char Exchange(int index, char value)
    {
#if DEBUG
        if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        if (index >= _bufferLength)
        {
            return '\0';
        }

        var result = _rawBuffer[index];
        this[index] = value;
        return result;
    }

    public void WriteChars(ReadOnlySpan<char> text, int destinationIndex)
    {
        EnsureBufferCapacity(text.Length + destinationIndex);

        text.CopyTo(_rawBuffer.AsSpan(destinationIndex));

        if (destinationIndex <= _terminatedLength)
        {
            _terminatedLength = -1;
        }
    }

    public void Assign(ReadOnlySpan<char> text)
    {
#if DEBUG
        if (text.Length > _bufferLength) throw new ArgumentOutOfRangeException(nameof(text));
#endif

        var buffer = _rawBuffer.AsSpan(0, _bufferLength);
        text.CopyTo(buffer);

        if (text.Length < buffer.Length)
        {
            buffer.Slice(text.Length).Clear();
        }

        _terminatedLength = -1;
    }

    public void Destroy()
    {
        ReturnBuffer(_rawBuffer);
    }

    private void EnsureBufferCapacity(int neededLength)
    {
        if (_bufferLength < neededLength)
        {
            if (_rawBuffer.Length < neededLength)
            {
                var newBuffer = GetBuffer(neededLength);
                Array.Copy(_rawBuffer, newBuffer, _bufferLength);
                ReturnBuffer(_rawBuffer);
                _rawBuffer = newBuffer;
            }

            _bufferLength = neededLength;
        }
    }
}
