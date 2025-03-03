using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("BufferLength = {BufferLength}, Text = {ToString()}")]
internal struct SimulatedCString
{
    public SimulatedCString(int capacity)
    {
#if DEBUG
        ExceptionEx.ThrowIfArgumentLessThan(capacity, 0, nameof(capacity));
#endif
        _rawBuffer = ArrayPool<char>.Shared.Rent(capacity);
        _bufferLength = capacity;
        _terminatedLength = 0;
    }

    public SimulatedCString(ReadOnlySpan<char> text)
    {
        _rawBuffer = ArrayPool<char>.Shared.Rent(text.Length + 3); // 3 extra characters seems to be enough to prevent most reallocations
        text.CopyTo(_rawBuffer);
        _bufferLength = text.Length;
        _terminatedLength = -1;
    }

    private char[] _rawBuffer;
    private int _bufferLength;
    private int _terminatedLength;

    public readonly int BufferLength => _bufferLength;

    public char this[int index]
    {
        readonly get
        {
#if DEBUG
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThan(index, _bufferLength, nameof(index)); // Allow access to the virtual or real null terminator
#endif
            return index < _bufferLength ? _rawBuffer[index] : '\0';
        }

        set
        {
#if DEBUG
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, _bufferLength, nameof(index));
#endif
            _rawBuffer[index] = value;

            if (value == '\0')
            {
                if (index == 0 || index < _terminatedLength)
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

    public readonly ReadOnlySpan<char> SliceToTerminatorFromOffset(int startIndex)
    {
#if DEBUG
        ExceptionEx.ThrowIfArgumentLessThan(startIndex, 0, nameof(startIndex));
        ExceptionEx.ThrowIfArgumentGreaterThan(startIndex, _bufferLength, nameof(startIndex));
#endif

        var result = _rawBuffer.AsSpan(startIndex, _bufferLength - startIndex);
        var index = result.IndexOf('\0');
        return index >= 0 ? result.Slice(0, index) : result;
    }

    public char ExchangeWithNull(int index)
    {
#if DEBUG
        ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
        ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, _bufferLength, nameof(index));
#endif

        if (index == 0 || index < _terminatedLength)
        {
            _terminatedLength = index;
        }

        return performExchange(ref _rawBuffer[index]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static char performExchange(ref char target)
        {
            var previous = target;
            target = '\0';
            return previous;
        }
    }

    public void WriteChars(ReadOnlySpan<char> text, int destinationIndex)
    {
#if DEBUG
        ExceptionEx.ThrowIfArgumentLessThan(destinationIndex, 0, nameof(destinationIndex));
        ExceptionEx.ThrowIfArgumentGreaterThan(destinationIndex, _bufferLength, nameof(destinationIndex));
#endif

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
        ExceptionEx.ThrowIfArgumentGreaterThan(text.Length, _bufferLength, nameof(text));
#endif

        var buffer = _rawBuffer.AsSpan(0, _bufferLength);
        text.CopyTo(buffer);

        if (text.Length < buffer.Length)
        {
            buffer.Slice(text.Length).Clear();
        }

        _terminatedLength = -1;
    }

    public void RemoveRange(int startIndex, int count)
    {
#if DEBUG
        ExceptionEx.ThrowIfArgumentLessThan(startIndex, 0, nameof(startIndex));
        ExceptionEx.ThrowIfArgumentGreaterThan(startIndex + count, _bufferLength, nameof(count));
#endif

        if (count > 0)
        {
            if (_terminatedLength >= startIndex)
            {
                _terminatedLength = -1;
            }

            var buffer = _rawBuffer.AsSpan(0, _bufferLength);
            buffer.Slice(startIndex + count).CopyTo(buffer.Slice(startIndex)); // shift the leftovers backwards
            buffer.Slice(buffer.Length - count).Clear(); // zero the freed space at the end
        }
    }

    public override string ToString() => TerminatedSpan.ToString();

    public void Dispose()
    {
        if (_rawBuffer.Length != 0)
        {
            ArrayPool<char>.Shared.Return(_rawBuffer);
            _rawBuffer = [];
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

                if (_rawBuffer.Length != 0)
                {
                    ArrayPool<char>.Shared.Return(_rawBuffer);
                }

                _rawBuffer = newBuffer;
            }

            _bufferLength = neededLength;
        }
    }
}
