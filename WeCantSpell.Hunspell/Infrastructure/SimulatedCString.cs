using System;

namespace WeCantSpell.Hunspell.Infrastructure;

struct SimulatedCString
{
    public SimulatedCString(string text)
    {
        _rawBuffer = new char[text.Length + 3]; // 3 extra characters seems to be enough to prevent most reallocations
        text.CopyTo(0, _rawBuffer, 0, text.Length);
        _bufferLength = text.Length;
        _terminatedLengthCache = null;
    }

    public SimulatedCString(ReadOnlySpan<char> text)
    {
        _rawBuffer = new char[text.Length + 3]; // 3 extra characters seems to be enough to prevent most reallocations

        text.CopyTo(_rawBuffer.AsSpan());

        _bufferLength = text.Length;
        _terminatedLengthCache = null;
    }

    private char[] _rawBuffer;
    private int? _terminatedLengthCache;
    private int _bufferLength;

    public ReadOnlySpan<char> BufferSpan => _rawBuffer.AsSpan(0, _bufferLength);

    public ReadOnlySpan<char> TerminatedSpan => _rawBuffer.AsSpan(0, _terminatedLengthCache ??= BufferSpan.FindNullTerminatedLength());

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
                if (index < _terminatedLengthCache)
                {
                    _terminatedLengthCache = index;
                }
            }
            else if (index == _terminatedLengthCache)
            {
                _terminatedLengthCache = null;
            }
        }
    }

    public char Exchange(int index, char value)
    {
        if (index < 0 || index >= _bufferLength)
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

        _terminatedLengthCache = null;
    }

    public void Assign(ReadOnlySpan<char> text)
    {
#if DEBUG
        if (text.Length > _bufferLength) throw new ArgumentOutOfRangeException(nameof(text));
#endif
        var buffer = _rawBuffer.AsSpan(0, _bufferLength);

        text.CopyTo(buffer);

        if (text.Length < _bufferLength)
        {
            buffer.Slice(text.Length).Clear();
        }

        _terminatedLengthCache = null;
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public void Destroy()
    {
        // I want to keep this method here in case a future implementation can make use of it.
    }

    public override string ToString() => TerminatedSpan.ToString();

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
