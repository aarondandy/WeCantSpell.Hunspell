using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class CharacterCollection :
        IReadOnlyCollection<char>
    {
        public static readonly CharacterCollection Empty = new CharacterCollection(ArrayEx<char>.Empty);

        private readonly char[] values;

        private CharacterCollection(char[] values)
        {
            this.values = values;
        }

        public char this[int index] => values[index];

        public int Count => values.Length;

        public bool HasChars => values.Length != 0;

        public bool IsEmpty => values.Length == 0;

        public static CharacterCollection TakeArray(char[] values)
        {
            Array.Sort(values);
            return new CharacterCollection(values);
        }

        public static CharacterCollection Create(string values) => TakeArray(values.ToCharArray());

        public bool Contains(char value) => Array.BinarySearch(values, value) >= 0;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<char> GetEnumerator() => new FastArrayEnumerator<char>(values);

        IEnumerator<char> IEnumerable<char>.GetEnumerator() => ((IEnumerable<char>)values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => values.GetEnumerator();
    }
}
