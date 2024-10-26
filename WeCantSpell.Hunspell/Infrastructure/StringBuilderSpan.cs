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

        _bufferRental = ArrayPool<char>.Shared.Rent(capacity);
        _bufferSpan = _bufferRental.AsSpan();
        _length = 0;
    }

    public StringBuilderSpan(scoped ReadOnlySpan<char> text)
    {
        _bufferRental = ArrayPool<char>.Shared.Rent(text.Length);
        _bufferSpan = _bufferRental.AsSpan();
        _length = text.Length;
        text.CopyTo(_bufferSpan);
    }

#if NO_STRING_SPAN

    public StringBuilderSpan(string text) : this(text is null ? [] : text.AsSpan())
    {
    }

#else

    public StringBuilderSpan(string text)
    {
        if (text is not { Length: > 0 })
        {
            _bufferRental = ArrayPool<char>.Shared.Rent(0);
            _bufferSpan = _bufferRental.AsSpan();
            _length = 0;
        }
        else
        {
            _bufferRental = ArrayPool<char>.Shared.Rent(text.Length);
            _bufferSpan = _bufferRental.AsSpan();
            _length = text.Length;
            text.CopyTo(_bufferSpan);
        }
    }

#endif

    private char[] _bufferRental;
    private Span<char> _bufferSpan;
    private int _length;

    public readonly int Length => _length;

    public readonly char this[int index]
    {
        get
        {
#if DEBUG
            if (index < 0 || index >= _length) throw new ArgumentOutOfRangeException(nameof(index));
#endif

            return _bufferSpan[index];
        }

        set
        {
#if DEBUG
            if (index < 0 || index >= _length) throw new ArgumentOutOfRangeException(nameof(index));
#endif

            _bufferSpan[index] = value;
        }
    }

    internal readonly Span<char> CurrentSpan => _bufferSpan.Slice(0, _length);

    public void Clear()
    {
        _length = 0;
    }


#if NO_STRING_SPAN

    public void Set(string value)
    {
        if (value is not { Length: > 0 })
        {
            _length = 0;
        }
        else
        {
            Set(value.AsSpan());
        }
    }

#else

    public void Set(string value)
    {
        if (value is not { Length: > 0 })
        {
            _length = 0;
        }
        else
        {
            if (_bufferSpan.Length < value.Length)
            {
                ResetAndReallocateBuffer(value.Length);
            }

            value.CopyTo(_bufferSpan);
            _length = value.Length;
        }
    }

#endif

    public void Set(scoped ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            _length = 0;
        }
        else
        {
            if (_bufferSpan.Length < value.Length)
            {
                ResetAndReallocateBuffer(value.Length);
            }

            value.CopyTo(_bufferSpan);
            _length = value.Length;
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

    public void Append(scoped ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            return;
        }

        if (value.Length + _length > _bufferSpan.Length)
        {
            GrowBufferToCapacity(value.Length + _length);
        }

        value.CopyTo(_bufferSpan.Slice(_length));
        _length += value.Length;
    }

    public void Append(char value)
    {
        if (_length + 1 > _bufferSpan.Length)
        {
            GrowBufferToCapacity(_length + 1);
        }

        _bufferSpan[_length] = value;
        _length++;
    }

    public void AppendLower(scoped ReadOnlySpan<char> value, CultureInfo cultureInfo)
    {
        var space = AppendSpaceForImmediateWrite(value.Length);
        var written = value.ToLower(space, cultureInfo);
        if (written < 0)
        {
            Append(value.ToString().ToLower(cultureInfo));
        }
        else if (written != space.Length)
        {
            failSafe();
        }

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        static void failSafe()
        {
            throw new InvalidOperationException("Failed to change case");
        }
    }

    public void AppendReversed(scoped ReadOnlySpan<char> value)
    {
        var space = AppendSpaceForImmediateWrite(value.Length);
        value.CopyToReversed(space);
    }

    public readonly void Replace(char oldChar, char newChar)
    {
        Replace(oldChar, newChar, 0, _length);
    }

    public readonly void Replace(char oldChar, char newChar, int startIndex)
    {
        Replace(oldChar, newChar, startIndex, _length - startIndex);
    }

    public readonly void Replace(char oldChar, char newChar, int startIndex, int count)
    {
#if DEBUG
        if (startIndex >= _length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex + count > _length) throw new ArgumentOutOfRangeException(nameof(count));
#endif

        _bufferSpan.Slice(startIndex, count).Replace(oldChar, newChar);
    }

    public void Replace(string oldText, string newText)
    {
        Replace(oldText.AsSpan(), newText.AsSpan());
    }

    public void Replace(string oldText, string newText, int startIndex)
    {
        Replace(oldText.AsSpan(), newText.AsSpan(), startIndex);
    }

    public void Replace(string oldText, string newText, int startIndex, int count)
    {
        Replace(oldText.AsSpan(), newText.AsSpan(), startIndex, count);
    }

    public void Replace(scoped ReadOnlySpan<char> oldText, scoped ReadOnlySpan<char> newText)
    {
        Replace(oldText, newText, 0, _length);
    }

    public void Replace(scoped ReadOnlySpan<char> oldText, scoped ReadOnlySpan<char> newText, int startIndex)
    {
        Replace(oldText, newText, startIndex, _length - startIndex);
    }

    public void Replace(scoped ReadOnlySpan<char> oldText, scoped ReadOnlySpan<char> newText, int startIndex, int count)
    {
#if DEBUG
        if (startIndex >= _length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex + count > _length) throw new ArgumentOutOfRangeException(nameof(count));
#endif

        if (_length != 0 && count != 0 && !oldText.IsEmpty)
        {
            if (oldText.Length == newText.Length)
            {
                ReplaceEqualSizeInternal(oldText, newText, startIndex, count);
            }
            else if (oldText.Length < newText.Length)
            {
                ReplaceIncreasingSizeInternal(oldText, newText, startIndex, count);
            }
            else
            {
                ReplaceDecreasingSizeInternal(oldText, newText, startIndex, count);
            }
        }
    }

    private readonly void ReplaceEqualSizeInternal(scoped ReadOnlySpan<char> oldText, scoped ReadOnlySpan<char> newText, int startIndex, int count)
    {
#if DEBUG
        if (count == 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (oldText.IsEmpty) throw new ArgumentOutOfRangeException(nameof(oldText));
        if (oldText.Length != newText.Length) throw new ArgumentOutOfRangeException(nameof(newText));
        if (_length == 0) throw new InvalidOperationException();
#endif

        var editableArea = _bufferSpan.Slice(startIndex, count);

        do
        {
            if (editableArea.IsEmpty)
            {
                return;
            }

            var searchIndex = editableArea.IndexOf(oldText);
            if (searchIndex < 0)
            {
                return;
            }

            if (searchIndex > 0)
            {
                editableArea = editableArea.Slice(searchIndex);
            }

            newText.CopyTo(editableArea);

            editableArea = editableArea.Slice(newText.Length);
        }
        while (true);
    }

    private void ReplaceIncreasingSizeInternal(scoped ReadOnlySpan<char> oldText, scoped ReadOnlySpan<char> newText, int startIndex, int count)
    {
#if DEBUG
        if (count == 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (oldText.IsEmpty) throw new ArgumentOutOfRangeException(nameof(oldText));
        if (oldText.Length >= newText.Length) throw new ArgumentOutOfRangeException(nameof(newText));
        if (_length == 0) throw new InvalidOperationException();
#endif

        var searchIgnoreSize = _length - startIndex - count;
        var replacementGrowthSize = newText.Length - oldText.Length;

        do
        {
            if (_length - searchIgnoreSize <= startIndex)
            {
                return;
            }

            var searchIndex = _bufferSpan.Slice(startIndex, _length - startIndex - searchIgnoreSize).IndexOf(oldText);
            if (searchIndex < 0)
            {
                return;
            }

            if (_length + replacementGrowthSize > _bufferSpan.Length)
            {
                GrowBufferToCapacity(_length + replacementGrowthSize);
            }

            var editableArea = _bufferSpan.Slice(startIndex + searchIndex);

            editableArea.Slice(oldText.Length, _length - startIndex - searchIndex - oldText.Length).CopyTo(editableArea.Slice(newText.Length));
            newText.CopyTo(editableArea);

            _length += replacementGrowthSize;

            startIndex += searchIndex + newText.Length;
        }
        while (true);
    }

    private void ReplaceDecreasingSizeInternal(scoped ReadOnlySpan<char> oldText, scoped ReadOnlySpan<char> newText, int startIndex, int count)
    {
#if DEBUG
        if (count == 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (oldText.IsEmpty) throw new ArgumentOutOfRangeException(nameof(oldText));
        if (oldText.Length <= newText.Length) throw new ArgumentOutOfRangeException(nameof(newText));
        if (_length == 0) throw new InvalidOperationException();
#endif

        var searchIgnoreSize = _length - startIndex - count;

        do
        {
            if (_length - searchIgnoreSize <= startIndex)
            {
                return;
            }

            var editableArea = _bufferSpan.Slice(startIndex, _length - startIndex);

            var searchIndex = editableArea.Slice(0, editableArea.Length - searchIgnoreSize).IndexOf(oldText);
            if (searchIndex < 0)
            {
                return;
            }

            if (searchIndex > 0)
            {
                editableArea = editableArea.Slice(searchIndex);
            }

            newText.CopyTo(editableArea);
            editableArea.Slice(oldText.Length).CopyTo(editableArea.Slice(newText.Length));

            _length -= oldText.Length - newText.Length;
            startIndex += searchIndex + newText.Length;
        }
        while (true);
    }

    public void RemoveAt(int index)
    {
#if DEBUG
        if (index < 0 || index >= _length) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        if ((index + 1) < _length)
        {
            _bufferSpan.Slice(index + 1, _length - index - 1).CopyTo(_bufferSpan.Slice(index));
        }

        _length--;
    }

    public void Remove(int startIndex, int count)
    {
#if DEBUG
        if (startIndex < 0 || startIndex >= _length) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (startIndex + count > _length) throw new ArgumentOutOfRangeException(nameof(count));
#endif

        if (count != 0)
        {
            if (count == 1)
            {
                RemoveAt(startIndex);
            }
            else
            {
                RemoveRangeInternal(startIndex, count);
            }
        }
    }

    private void RemoveRangeInternal(int startIndex, int count)
    {
        var endIndex = startIndex + count;
        if (_length > endIndex)
        {
            _bufferSpan.Slice(endIndex, _length - endIndex).CopyTo(_bufferSpan.Slice(startIndex));
        }

        _length -= count;
    }

    public void Insert(int index, char value)
    {
#if DEBUG
        if (index < 0 || index > (_length + 1)) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        if (_length + 1 > _bufferSpan.Length)
        {
            GrowBufferToCapacity(_length + 1);
        }

        if (index < _length)
        {
            _bufferSpan.Slice(index, _length - index).CopyTo(_bufferSpan.Slice(index + 1));
        }

        _bufferSpan[index] = value;
        _length++;
    }

    public void Insert(int index, scoped ReadOnlySpan<char> value)
    {
#if DEBUG
        if (index < 0 || index > (_length + 1)) throw new ArgumentOutOfRangeException(nameof(index));
#endif

        var newSize = value.Length + _length;
        if (newSize > _bufferSpan.Length)
        {
            GrowBufferToCapacity(newSize);
        }

        if (index < _length)
        {
            _bufferSpan.Slice(index, _length - index).CopyTo(_bufferSpan.Slice(value.Length + index));
        }

        value.CopyTo(_bufferSpan.Slice(index));
        _length = newSize;
    }

    public override readonly string ToString() => _length == 0 ? string.Empty : _bufferSpan.Slice(0, _length).ToString();

    public readonly bool StartsWith(char value) => _length != 0 && _bufferSpan[0] == value;

    public readonly bool EndsWith(char value) => _length != 0 && _bufferSpan[_length - 1] == (value);

    public string GetStringAndDispose()
    {
        var result = ToString();
        Dispose();
        return result;
    }

    public void Dispose()
    {
        var toReturn = _bufferRental;

        this = default;

        if (toReturn is not null)
        {
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    private Span<char> AppendSpaceForImmediateWrite(int size)
    {
        var newSize = _length + size;
        if (_bufferSpan.Length < newSize)
        {
            GrowBufferToCapacity(newSize);
        }

        var newSpace = _bufferSpan.Slice(_length, size);

        _length = newSize;

        return newSpace;
    }

    private void GrowBufferToCapacity(int capacity)
    {
#if DEBUG
        if (_bufferSpan.Length >= capacity) throw new InvalidOperationException();
#endif

        var oldBuffer = _bufferRental;
        var newBuffer = ArrayPool<char>.Shared.Rent(capacity);
        var newSpan = newBuffer.AsSpan();

        _bufferSpan.Slice(0, _length).CopyTo(newSpan);
        _bufferRental = newBuffer;
        _bufferSpan = newSpan;

        if (oldBuffer is not null)
        {
            ArrayPool<char>.Shared.Return(oldBuffer);
        }
    }

    private void ResetAndReallocateBuffer(int capacity)
    {
        var oldBuffer = _bufferRental;

        _bufferRental = ArrayPool<char>.Shared.Rent(capacity);
        _bufferSpan = _bufferRental.AsSpan();
        _length = 0;

        if (oldBuffer is not null)
        {
            ArrayPool<char>.Shared.Return(oldBuffer);
        }
    }
}
