using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public readonly struct CharacterSet : IReadOnlyList<char>, IEquatable<CharacterSet>
{
    public static readonly CharacterSet Empty = new(Array.Empty<char>(), default);

    public static bool operator ==(CharacterSet left, CharacterSet right) => left.Equals(right);

    public static bool operator !=(CharacterSet left, CharacterSet right) => !(left == right);

    public static CharacterSet Create(char value) => new(new[] { value }, value);

    public static CharacterSet Create(IEnumerable<char> values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        var builder = new Builder();
        builder.AddRange(values);
        return builder.Create(allowDestructive: true);
    }

    public static CharacterSet Create(string values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

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

    private CharacterSet(char[] values, char mask)
    {
        _mask = mask;
        _values = values;
    }

    private readonly char[] _values;
    private readonly char _mask;

    public int Count => _values.Length;
    public bool IsEmpty => !HasItems;
    public bool HasItems => _values is { Length: > 0 };
    public char this[int index] => _values[index];
    public IEnumerator<char> GetEnumerator() => ((IEnumerable<char>)_values).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();

    public bool Contains(char value)
    {
        if (_values is not null)
        {
            if (_values.Length == 1)
            {
                return _values[0].Equals(value);
            }

            if (unchecked((value & _mask) == value))
            {
                return _values.Length <= 8
                    ? checkIterative(_values, value)
                    : Array.BinarySearch(_values, value) >= 0;
            }
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

        if (index == textSpan.Length - 1)
        {
            return text.Substring(0, index);
        }

        var builder = StringBuilderPool.Get(textSpan.Length - 1);

        do
        {
            builder.Append(textSpan.Slice(0, index));
            textSpan = textSpan.Slice(index + 1);
        }
        while ((index = FindIndexOfMatch(textSpan)) >= 0);

        builder.Append(textSpan);

        return StringBuilderPool.GetStringAndReturn(builder);
    }

    public ReadOnlySpan<char> RemoveChars(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty || IsEmpty)
        {
            return text;
        }

        var index = FindIndexOfMatch(text);
        if (index < 0)
        {
            return text;
        }

        if (index == text.Length - 1)
        {
            return text.Slice(0, index);
        }

        var builder = StringBuilderPool.Get(text.Length - 1);

        do
        {
            builder.Append(text.Slice(0, index));
            text = text.Slice(index + 1);
        }
        while ((index = FindIndexOfMatch(text)) >= 0);

        builder.Append(text);

        return StringBuilderPool.GetStringAndReturn(builder).AsSpan();
    }

    public override string ToString() => new(_values);

    public bool Equals(CharacterSet obj) => _values.SequenceEqual(obj._values);

    public override bool Equals(object? obj) => obj is CharacterSet set && Equals(set);

    public override int GetHashCode() => HashCode.Combine(Count, _mask);

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
        private char _mask = default;

        public void AddRange(IEnumerable<char> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

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

        public void Add(char value)
        {
            _builder.AddAsSortedSet(value);
            unchecked
            {
                _mask |= value;
            }
        }

        public CharacterSet Create() => Create(allowDestructive: false);

        internal CharacterSet Create(bool allowDestructive)
        {
            CharacterSet result;
            if (allowDestructive)
            {
                result = new(_builder.Extract(), _mask);
                _mask = default;
            }
            else
            {
                result = new(_builder.MakeArray(), _mask);
            }

            return result;
        }
    }
}
