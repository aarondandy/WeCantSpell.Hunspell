using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterSet : IReadOnlyList<char>, IEquatable<CharacterSet>
{
    public static readonly CharacterSet Empty = new([]);

    public static bool operator ==(CharacterSet left, CharacterSet right) => left.Equals(right);

    public static bool operator !=(CharacterSet left, CharacterSet right) => !(left == right);

    public static CharacterSet Create(char value) => new(value);

    public static CharacterSet Create(IEnumerable<char> values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        var builder = new Builder();
        builder.AddRange(values);
        return builder.Create(allowDestructive: true);
    }

    public static CharacterSet Create(string values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        return Create(values.AsSpan());
    }

    public static CharacterSet Create(ReadOnlySpan<char> values)
    {
        if (values.IsEmpty)
        {
            return Empty;
        }

        var builder = new Builder(values.Length);
        builder.AddRange(values);
        return builder.Create(allowDestructive: true);
    }

#if HAS_SEARCHVALUES

    private CharacterSet(char value) : this([value])
    {
    }

    private CharacterSet(char[] values)
    {
        _values = values;
        _searchValues = SearchValues.Create(values);
    }

    private readonly char[]? _values;
    private readonly SearchValues<char>? _searchValues;

#else

    private CharacterSet(char value) : this([value])
    {
    }

    private CharacterSet(char[] values)
    {
        _values = values;
    }

    private readonly char[]? _values;

#endif

    public int Count => _values is null ? 0 : _values.Length;
    public bool IsEmpty => _values is not { Length: > 0 };
    public bool HasItems => _values is { Length: > 0 };
    public char this[int index]
    {
        get
        {
#if HAS_THROWOOR
            ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
#else
            ExceptionEx.ThrowIfArgumentLessThan(index, 0, nameof(index));
            ExceptionEx.ThrowIfArgumentGreaterThanOrEqual(index, Count, nameof(index));
#endif
            return _values![index];
        }
    }

    public IEnumerator<char> GetEnumerator() => ((IEnumerable<char>)GetInternalArray()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private char[] GetInternalArray() => _values ?? [];

#if HAS_SEARCHVALUES

    public bool Contains(char value) => _searchValues is not null && _searchValues.Contains(value);

    public int FindIndexOfMatch(ReadOnlySpan<char> text) => _searchValues is not null ? text.IndexOfAny(_searchValues) : -1;

#else

    public bool Contains(char value)
    {
        if (_values is not null)
        {
            if (_values.Length == 1)
            {
                return _values[0].Equals(value);
            }

            return _values.Length <= 8
                ? checkIterative(_values, value)
                : Array.BinarySearch(_values, value) >= 0;
        }

        return false;

        static bool checkIterative(char[] searchSpace, char target)
        {
            foreach (var value in searchSpace)
            {
                if (value == target)
                {
                    return true;
                }
                if (value > target)
                {
                    break;
                }
            }

            return false;
        }
    }

    public int FindIndexOfMatch(ReadOnlySpan<char> text)
    {
        if (_values is not null)
        {
            switch (_values.Length)
            {
                case 1:
                    return text.IndexOf(_values[0]);
                case <= 8:
                    return text.IndexOfAny(_values.AsSpan());
                default:
                    for (var searchLocation = 0; searchLocation < text.Length; searchLocation++)
                    {
                        if (Contains(text[searchLocation]))
                        {
                            return searchLocation;
                        }
                    }

                    break;
            }
        }

        return -1;
    }

#endif

    public int FindIndexOfMatch(ReadOnlySpan<char> text, int startIndex)
    {
        var result = FindIndexOfMatch(text.Slice(startIndex));
        return result < 0 ? result : result + startIndex;
    }

    public string RemoveChars(string text)
    {
        if (text is not { Length: > 0 } || IsEmpty)
        {
            return text;
        }

        var textSpan = text.AsSpan();
        var index = FindIndexOfMatch(textSpan);
        if (index < 0)
        {
            return text;
        }

        do
        {
            if (index == textSpan.Length - 1)
            {
                return textSpan.Slice(0, index).ToString();
            }

            if (index != 0)
            {
                break;
            }

            if (textSpan.Length <= 1)
            {
                return string.Empty;
            }

            textSpan = textSpan.Slice(1);

            index = FindIndexOfMatch(textSpan);
            if (index < 0)
            {
                return textSpan.ToString();
            }
        }
        while (true);

        return ToStringWithRemoval(textSpan, index);
    }

    public ReadOnlySpan<char> RemoveChars(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty || IsEmpty)
        {
            return text;
        }

        int index;
        do
        {
            index = FindIndexOfMatch(text);
            if (index < 0)
            {
                return text;
            }

            if (index == text.Length - 1)
            {
                return text.Slice(0, index);
            }

            if (index != 0)
            {
                break;
            }

            if (text.Length <= 1)
            {
                return [];
            }

            text = text.Slice(1);
        }
        while (true);

        return ToStringWithRemoval(text, index).AsSpan();
    }

    private string ToStringWithRemoval(ReadOnlySpan<char> text, int matchIndex)
    {
        var builder = new StringBuilderSpan(text.Length - 1);

        do
        {
            if (matchIndex > 0)
            {
                builder.Append(text.Slice(0, matchIndex));
            }

            text = text.Slice(matchIndex + 1);
            matchIndex = FindIndexOfMatch(text);
        }
        while (matchIndex >= 0);

        builder.Append(text);

        return builder.GetStringAndDispose();
    }

    public override string ToString() => new(GetInternalArray());

    public bool Equals(CharacterSet obj) => GetInternalArray().SequenceEqual(obj.GetInternalArray());

    public override bool Equals(object? obj) => obj is CharacterSet set && Equals(set);

    public override int GetHashCode() =>
#if HAS_SEARCHVALUES
        HashCode.Combine(Count, HasItems ? _values![0] : default);
#else
        (int)StringEx.GetStableOrdinalHashCode(_values);
#endif

    public sealed class Builder
    {
        public Builder()
        {
            _builder = new ArrayBuilder<char>();
        }

        public Builder(int capacity)
        {
            _builder = capacity is >= 0 and <= CollectionsEx.CollectionPreallocationLimit
                ? new ArrayBuilder<char>(capacity)
                : new ArrayBuilder<char>();
        }

        private readonly ArrayBuilder<char> _builder;

#if HAS_SEARCHVALUES

        public void Add(char value)
        {
            _builder.AddAsSortedSet(value);
        }

        internal CharacterSet Create(bool allowDestructive)
        {
            CharacterSet result;
            if (allowDestructive)
            {
                result = new(_builder.Extract());
            }
            else
            {
                result = new(_builder.MakeArray());
            }

            return result;
        }

#else

        public void Add(char value)
        {
            _builder.AddAsSortedSet(value);
        }

        internal CharacterSet Create(bool allowDestructive)
        {
            return allowDestructive
                ? new(_builder.Extract())
                : new(_builder.MakeArray());
        }

#endif

        public void AddRange(IEnumerable<char> values)
        {
#if HAS_THROWNULL
            ArgumentNullException.ThrowIfNull(values);
#else
            ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

            foreach (var value in values)
            {
                Add(value);
            }
        }

        public void AddRange(ReadOnlySpan<char> values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        public CharacterSet Create() => Create(allowDestructive: false);
    }
}
