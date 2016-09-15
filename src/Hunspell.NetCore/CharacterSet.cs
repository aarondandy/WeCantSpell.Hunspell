using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class CharacterSet :
        IReadOnlyCollection<char>
    {
        public static readonly CharacterSet Empty = new CharacterSet(ArrayEx<char>.Empty);

        private readonly char[] values;

        private CharacterSet(char[] values)
        {
            this.values = values;
        }

        public char this[int index] => values[index];

        public int Count => values.Length;

        public bool HasChars => values.Length != 0;

        public bool IsEmpty => values.Length == 0;

        public static CharacterSet TakeArray(char[] values)
        {
            Array.Sort(values);
            return new CharacterSet(values);
        }

        public static CharacterSet Create(string values) => TakeArray(values.ToCharArray());

        public static CharacterSet Create(char value) => TakeArray(new[] { value });

        public bool Contains(char value) => Array.BinarySearch(values, value) >= 0;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<char> GetEnumerator() => new FastArrayEnumerator<char>(values);

        IEnumerator<char> IEnumerable<char>.GetEnumerator() => ((IEnumerable<char>)values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => values.GetEnumerator();
    }
}
