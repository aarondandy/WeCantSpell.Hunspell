using System;
using System.Buffers;

namespace WeCantSpell.Hunspell.Infrastructure;

struct SimulatedCString
{
    public SimulatedCString(int capacity)
    {
        if (capacity <= 0)
        {
            capacity = 1;
        }

        _rawBuffer = ArrayPool<char>.Shared.Rent(capacity);
        _bufferLength = capacity;
        _rawBuffer[0] = '\0';
        _terminatedLength = 0;
    }

    public SimulatedCString(ReadOnlySpan<char> text)
    {
        _rawBuffer = ArrayPool<char>.Shared.Rent(text.Length + 3); // 3 extra characters seems to be enough to prevent most reallocations
        text.CopyTo(_rawBuffer.AsSpan(0, text.Length));
        _bufferLength = text.Length;
        _terminatedLength = -1;
    }

    private char[] _rawBuffer;
    private int _bufferLength;
    private int _terminatedLength;

    public readonly int BufferLength => _bufferLength;

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
        readonly get => index < _bufferLength ? _rawBuffer[index] : '\0';
        set
        {
#if DEBUG
            if (index >= _bufferLength) throw new ArgumentOutOfRangeException(nameof(index));
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

    public readonly ReadOnlySpan<char> SliceToTerminator(int startIndex)
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
        if (_rawBuffer.Length != 0)
        {
            ArrayPool<char>.Shared.Return(_rawBuffer);
            _rawBuffer = [];
            _bufferLength = 0;
        }
    }

    private void EnsureBufferCapacity(int neededLength)
    {
        if (_bufferLength < neededLength)
        {
            if (_rawBuffer.Length < neededLength)
            {
                var newBuffer = ArrayPool<char>.Shared.Rent(neededLength);
                Array.Copy(_rawBuffer, newBuffer, _bufferLength);
                ArrayPool<char>.Shared.Return(_rawBuffer);
                _rawBuffer = newBuffer;
            }

            _bufferLength = neededLength;
        }
    }
}
