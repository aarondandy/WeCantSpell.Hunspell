using System;
using System.Buffers;
using System.Globalization;

namespace WeCantSpell.Hunspell.Infrastructure;

ref struct StringBuilderSpan
{
    public StringBuilderSpan(int capacity)
    {
#if DEBUG
        if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
#endif

        _rawBuffer = ArrayPool<char>.Shared.Rent(capacity);
        _chars = [];
    }

    public StringBuilderSpan(ReadOnlySpan<char> text)
    {
        _rawBuffer = ArrayPool<char>.Shared.Rent(text.Length);
        _chars = _rawBuffer.AsSpan(0, text.Length);
        text.CopyTo(_chars);
    }

    public StringBuilderSpan(string text)
    {
        if (text is not { Length: > 0 })
        {
            _rawBuffer = ArrayPool<char>.Shared.Rent(0);
            _chars = [];
        }
        else
        {
            _rawBuffer = ArrayPool<char>.Shared.Rent(text.Length);
            _chars = _rawBuffer.AsSpan(0, text.Length);
            text.AsSpan().CopyTo(_chars);
        }
    }

    private char[] _rawBuffer;
    private Span<char> _chars;

    public readonly int Length => _chars.Length;

    public char this[int index]
    {
        readonly get => _chars[index];
        set => _chars[index] = value;
    }

    public void Clear()
    {
        _chars = [];
    }

    public void Set(string value)
    {
        if (value is not { Length: > 0 })
        {
            _chars = [];
        }
        else
        {
            Set(value.AsSpan());
        }
    }

    public void Set(ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            _chars = [];
        }
        else
        {
            if (_rawBuffer.Length < value.Length)
            {
                GrowBufferToCapacity(value.Length);
            }

            _chars = _rawBuffer.AsSpan(0, value.Length);
            value.CopyTo(_chars);
        }
    }

    public void Append(string value)
    {
        if (value is not { Length: > 0 })
        {
            return;
        }

        Append(value.AsSpan());
    }

    public void Append(ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            return;
        }

        var newSize = _chars.Length + value.Length;
        if (_rawBuffer.Length < newSize)
        {
            GrowBufferToCapacity(newSize);
        }

        value.CopyTo(_rawBuffer.AsSpan(_chars.Length));
        _chars = _rawBuffer.AsSpan(0, newSize);
    }

    public void Append(char value)
    {
        var newSize = _chars.Length + 1;
        if (_rawBuffer.Length < newSize)
        {
            GrowBufferToCapacity(newSize);
        }

        _rawBuffer[_chars.Length] = value;
        _chars = _rawBuffer.AsSpan(0, newSize);
    }

    public void AppendLower(ReadOnlySpan<char> value, CultureInfo cultureInfo)
    {
        var space = AppendSpaceForImmediateWrite(value.Length);
        value.ToLower(space, cultureInfo);
    }

    public void AppendUpper(ReadOnlySpan<char> value, CultureInfo cultureInfo)
    {
        var space = AppendSpaceForImmediateWrite(value.Length);
        value.ToUpper(space, cultureInfo);
    }

    public void AppendReversed(ReadOnlySpan<char> value)
    {
        var space = AppendSpaceForImmediateWrite(value.Length);
        value.CopyToReversed(space);
    }

    public void Replace(char oldChar, char newChar, int startIndex, int count)
    {
        _chars.Slice(startIndex, count).Replace(oldChar, newChar);
    }

    public void Replace(string oldText, string newText, int startIndex, int count)
    {
        Replace(oldText.AsSpan(), newText.AsSpan(), startIndex, count);
    }

    public void Replace(ReadOnlySpan<char> oldText, ReadOnlySpan<char> newText, int startIndex, int count)
    {
        if (_chars.IsEmpty || oldText.IsEmpty)
        {
            return;
        }

        do
        {
            if (startIndex >= _chars.Length)
            {
                return;
            }

            startIndex = _chars.IndexOf(oldText, startIndex);
            if (startIndex < 0)
            {
                return;
            }

            // This isn't very optimal but it should be good enough
            if (oldText.Length == newText.Length)
            {
                newText.CopyTo(_chars.Slice(startIndex));
            }
            else if (newText.Length < oldText.Length)
            {
                newText.CopyTo(_chars.Slice(startIndex));
                Remove(startIndex + newText.Length, oldText.Length - newText.Length);
            }
            else
            {
                Remove(startIndex, oldText.Length);
                Insert(startIndex, newText);
            }

            startIndex += newText.Length;
        }
        while (true);
    }

    public void Remove(int startIndex, int count)
    {
#if DEBUG
        if (startIndex < 0 || startIndex >= _chars.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex + count > _chars.Length) throw new ArgumentOutOfRangeException(nameof(count));
#endif

        if (count == 0)
        {
            return;
        }

        var endIndex = startIndex + count;
        if (_chars.Length > endIndex)
        {
            _chars.Slice(endIndex).CopyTo(_chars.Slice(startIndex));
        }

        _chars = _chars.Slice(0, _chars.Length - count);
    }

    public void Insert(int index, char value)
    {
#if DEBUG
        if (index < 0 || index > (_chars.Length + 1)) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        if (_chars.Length + 1 > _rawBuffer.Length)
        {
            GrowBufferToCapacity(_chars.Length + 1);
        }

        var newChars = _rawBuffer.AsSpan(0, _chars.Length + 1);

        if (index < _chars.Length)
        {
            newChars.Slice(index, newChars.Length - index - 1).CopyTo(newChars.Slice(index + 1));
        }

        newChars[index] = value;

        _chars = newChars;
    }

    public void Insert(int index, ReadOnlySpan<char> value)
    {
#if DEBUG
        if (index < 0 || index > (_chars.Length + 1)) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        var newSize = _chars.Length + value.Length;
        if (_rawBuffer.Length < newSize)
        {
            GrowBufferToCapacity(newSize);
        }

        var newChars = _rawBuffer.AsSpan(0, newSize);

        if (index < _chars.Length)
        {
            newChars.Slice(index, newChars.Length - index - value.Length).CopyTo(newChars.Slice(index + value.Length));
        }

        value.CopyTo(newChars.Slice(index));

        _chars = newChars;
    }

    public override readonly string ToString() => _chars.ToString();

    public readonly bool EndsWith(char value) => _chars.EndsWith(value);

    public readonly bool StartsWith(char value) => _chars.StartsWith(value);

    public string GetStringAndDispose()
    {
        var result = ToString();
        Dispose();
        return result;
    }

    public void Dispose()
    {
        var toReturn = _rawBuffer;

        this = default;

        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    private Span<char> AppendSpaceForImmediateWrite(int size)
    {
        var newSize = _chars.Length + size;
        if (_rawBuffer.Length < newSize)
        {
            GrowBufferToCapacity(newSize);
        }

        var newSpace = _rawBuffer.AsSpan(_chars.Length, size);

        _chars = _rawBuffer.AsSpan(0, newSize);

        return newSpace;
    }

    private void GrowBufferToCapacity(int capacity)
    {
#if DEBUG
        if (_rawBuffer.Length >= capacity) throw new InvalidOperationException();
#endif

        var newBuffer = ArrayPool<char>.Shared.Rent(capacity);
        var newChars = newBuffer.AsSpan(0, _chars.Length);

        var oldBuffer = _rawBuffer;
        _chars.CopyTo(newChars);

        _rawBuffer = newBuffer;
        _chars = newChars;

        if (oldBuffer is not null)
        {
            ArrayPool<char>.Shared.Return(oldBuffer);
        }
    }
}
