using System;
using System.Buffers;
using System.Threading;

namespace WeCantSpell.Hunspell.Infrastructure;

struct SimulatedCString
{
    public SimulatedCString(string text)
    {
        _rawBuffer = ArrayPool<char>.Shared.Rent(text.Length);
        text.AsSpan().CopyTo(_rawBuffer.AsSpan(0, text.Length));
        _bufferLength = text.Length;
        _terminatedLength = null;
    }

    private char[] _rawBuffer;
    private int? _terminatedLength;
    private int _bufferLength;

    public ReadOnlySpan<char> BufferSpan => _rawBuffer.AsSpan(0, _bufferLength);

    public ReadOnlySpan<char> TerminatedSpan => _rawBuffer.AsSpan(0, _terminatedLength ??= BufferSpan.FindNullTerminatedLength());

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
                _terminatedLength = null;
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

        _terminatedLength = null;
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

        _terminatedLength = null;
    }

    public void Destroy()
    {
        var oldBuffer = Interlocked.Exchange(ref _rawBuffer, Array.Empty<char>());
        if (oldBuffer.Length > 0)
        {
            ArrayPool<char>.Shared.Return(oldBuffer);
        }
    }

    public override string ToString() => TerminatedSpan.ToString();

    private void EnsureBufferCapacity(int neededLength)
    {
        if (_bufferLength < neededLength)
        {
            if (_rawBuffer.Length < neededLength)
            {
                RebuildRawBuffer(neededLength);
            }

            _bufferLength = neededLength;
        }
    }

    private void RebuildRawBuffer(int neededLength)
    {
        var newRawBuffer = ArrayPool<char>.Shared.Rent(neededLength);
        var newBuffer = newRawBuffer.AsSpan(0, neededLength);
        var oldRawBuffer = _rawBuffer;

        oldRawBuffer.AsSpan(0, _bufferLength).CopyTo(newBuffer);
        _rawBuffer = newRawBuffer;

        ArrayPool<char>.Shared.Return(oldRawBuffer);
    }
}
