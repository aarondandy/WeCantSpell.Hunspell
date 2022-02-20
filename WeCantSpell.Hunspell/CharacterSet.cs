﻿using System;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class CharacterSet : ArrayWrapper<char>
    {
        public static readonly CharacterSet Empty = new CharacterSet(ArrayEx<char>.Empty);

        public static readonly ArrayWrapperComparer<char, CharacterSet> DefaultComparer = new ArrayWrapperComparer<char, CharacterSet>();

        public static CharacterSet Create(string values) => values == null ? Empty : TakeArray(values.ToCharArray());

        public static CharacterSet Create(char value) => TakeArray(new[] { value });

        internal static CharacterSet Create(ReadOnlySpan<char> values) => TakeArray(values.ToArray());

        internal static CharacterSet TakeArray(char[] values)
        {
#if DEBUG
            if (values == null) throw new ArgumentNullException(nameof(values));
#endif

            Array.Sort(values);
            return new CharacterSet(values);
        }

        private CharacterSet(char[] values)
            : base(values)
        {
            mask = default;
            for (var i = 0; i < values.Length; i++)
            {
                unchecked
                {
                    mask |= values[i];
                }
            }
        }

        private readonly char mask;

        public bool Contains(char value) =>
            unchecked((value & mask) != default)
            &&
            Array.BinarySearch(items, value) >= 0;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public string GetCharactersAsString() => new string(items);
    }
}
