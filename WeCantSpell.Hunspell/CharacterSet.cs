using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterSet : IReadOnlyList<char>, IEquatable<CharacterSet>, IEquatable<string>
{
    public static readonly CharacterSet Empty = new(string.Empty);

    public static bool operator ==(CharacterSet left, CharacterSet right) => left.Equals(right);

    public static bool operator !=(CharacterSet left, CharacterSet right) => !left.Equals(right);

    public static CharacterSet Create(char value) => new(value.ToString());

    public static CharacterSet Create(IEnumerable<char> values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        var builder = new StringBuilderSpan(values.GetNonEnumeratedCountOrDefault());
        builder.Append(values);
        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose());
    }

    public static CharacterSet Create(string values)
    {
#if HAS_THROWNULL
        ArgumentNullException.ThrowIfNull(values);
#else
        ExceptionEx.ThrowIfArgumentNull(values, nameof(values));
#endif

        var valuesSpan = values.AsSpan();
        return valuesSpan.CheckSortedWithoutDuplicates()
            ? new(values)
            : Create(valuesSpan);
    }

    public static CharacterSet Create(ReadOnlySpan<char> values)
    {
        if (values.IsEmpty)
        {
            return Empty;
        }

        var builder = new StringBuilderSpan(values);
        builder.Sort();
        builder.RemoveAdjacentDuplicates();
        return new(builder.GetStringAndDispose());
    }

#if HAS_SEARCHVALUES

    private CharacterSet(string values)
    {
        _values = values;
        _searchValues = SearchValues.Create(values);
    }

    private readonly string? _values;
    private readonly SearchValues<char>? _searchValues;

#else

    private CharacterSet(string values)
    {
        _values = values;
    }

    private readonly string? _values;

#endif

    public int Count => _values is not null ? _values.Length : 0;

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

            if (_values is null)
            {
                ExceptionEx.ThrowInvalidOperation("Not initialized");
            }

            return _values![index];
        }
    }

    public IEnumerator<char> GetEnumerator() => ToString().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if HAS_SEARCHVALUES

    public bool Contains(char value) => _searchValues!.Contains(value);

    public int FindIndexOfMatch(ReadOnlySpan<char> text) => text.IndexOfAny(_searchValues!);

#else

    public bool Contains(char value) => MemoryEx.SortedLargeSearchSpaceContains(_values.AsSpan(), value);

    public int FindIndexOfMatch(ReadOnlySpan<char> text)
    {
        if (_values is not null)
        {
            switch (_values.Length)
            {
                case 1:
                    return text.IndexOf(_values[0]);
                case <= 5: // There are special cases in IndexOfAny for sizes 5 or less
                    return text.IndexOfAny(_values.AsSpan());
                default:
                    var values = _values.AsSpan();
                    for (var searchLocation = 0; searchLocation < text.Length; searchLocation++)
                    {
                        if (MemoryEx.SortedLargeSearchSpaceContains(values, text[searchLocation]))
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

    public override string ToString() => _values ?? string.Empty;

    public bool Equals(CharacterSet other) => ToString().Equals(other.ToString(), StringComparison.Ordinal);

    public bool Equals(string? other) => other is not null && ToString().Equals(other, StringComparison.Ordinal);

    public override bool Equals(object? obj) => obj switch
    {
        CharacterSet set => Equals(set),
        string value => Equals(value),
        _ => false
    };

    public override int GetHashCode() => unchecked((int)StringEx.GetStableOrdinalHashCode(ToString()));
}
