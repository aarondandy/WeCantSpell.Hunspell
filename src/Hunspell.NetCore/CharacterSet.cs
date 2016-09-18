using System;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class CharacterSet : ArrayWrapper<char>
    {
        public static readonly CharacterSet Empty = new CharacterSet(ArrayEx<char>.Empty);

        private CharacterSet(char[] values)
            : base(values)
        {
        }

        internal static CharacterSet TakeArray(char[] values)
        {
            if (values == null)
            {
                return Empty;
            }

            Array.Sort(values);
            return new CharacterSet(values);
        }

        public static CharacterSet Create(string values) => values == null ? Empty : TakeArray(values.ToCharArray());

        internal static CharacterSet Create(StringSlice values) => TakeArray(values.ToCharArray());

        public static CharacterSet Create(char value) => TakeArray(new[] { value });

        public bool Contains(char value) => Array.BinarySearch(items, value) >= 0;
    }
}
