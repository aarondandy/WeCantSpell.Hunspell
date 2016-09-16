using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class MapEntry :
        IReadOnlyList<string>
    {
        public static readonly MapEntry Empty = TakeArray(ArrayEx<string>.Empty);

        private readonly string[] values;

        private MapEntry(string[] values)
        {
            this.values = values;
        }

        public string this[int index] => values[index];

        public int Count => values.Length;

        internal static MapEntry TakeArray(string[] values) => new MapEntry(values);

        public static MapEntry Create(List<string> values) => TakeArray(values.ToArray());

        public static MapEntry Create(IEnumerable<string> values) => TakeArray(values.ToArray());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<string> GetEnumerator() => new FastArrayEnumerator<string>(values);

        IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)values).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => values.GetEnumerator();
    }
}
