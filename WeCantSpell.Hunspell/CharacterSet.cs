using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class CharacterSet : ArrayWrapper<char>
{
    public static readonly CharacterSet Empty = new CharacterSet(Array.Empty<char>());

    public static readonly ArrayWrapperComparer<char, CharacterSet> DefaultComparer = new ArrayWrapperComparer<char, CharacterSet>();

    public static CharacterSet Create(string values) => values is null ? Empty : TakeArray(values.ToCharArray());

    public static CharacterSet Create(char value) => TakeArray(new[] { value });

    internal static CharacterSet Create(ReadOnlySpan<char> values) => TakeArray(values.ToArray());

    internal static CharacterSet TakeArray(char[] values)
    {
#if DEBUG
        if (values is null) throw new ArgumentNullException(nameof(values));
#endif

        Array.Sort(values);
        return new CharacterSet(values);
    }

    private CharacterSet(char[] values) : base(values)
    {
        _mask = default;
        for (var i = 0; i < values.Length; i++)
        {
            unchecked
            {
                _mask |= values[i];
            }
        }
    }

    private readonly char _mask;

    public bool Contains(char value) =>
        unchecked((value & _mask) != default)
        &&
        Array.BinarySearch(Items, value) >= 0;

    public string GetCharactersAsString() => new string(Items);
}
