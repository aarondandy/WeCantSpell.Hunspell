using System;

namespace WeCantSpell.Hunspell.Infrastructure;

struct SimulatedCString
{
    public SimulatedCString(ReadOnlySpan<char> text)
    {
        _rawBuffer = new char[text.Length + 3]; // 3 extra characters seems to be enough to prevent most reallocations
        text.CopyTo(_rawBuffer.AsSpan(0, text.Length));
        _bufferLength = text.Length;
        _terminatedLength = -1;
    }

    private char[] _rawBuffer;
    private int _bufferLength;
    private int _terminatedLength;

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

    [System.Diagnostics.Conditional("DEBUG")]
    public void Destroy()
    {
        // I want to keep this method here in case a future implementation can make use of it.
    }

    private void EnsureBufferCapacity(int neededLength)
    {
        if (_bufferLength < neededLength)
        {
            if (_rawBuffer.Length < neededLength)
            {
                Array.Resize(ref _rawBuffer, neededLength);
            }

            _bufferLength = neededLength;
        }
    }
}
