using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class MorphSet
        : IReadOnlyList<string>
    {
        public static readonly MorphSet Empty = TakeArray(ArrayEx<string>.Empty);

        private readonly string[] morphs;

        private MorphSet(string[] morphs)
        {
            this.morphs = morphs;
        }

        public string this[int index] => morphs[index];

        public int Count => morphs.Length;

        public bool HasMorphs => morphs.Length != 0;

        public static MorphSet TakeArray(string[] morphs) => new MorphSet(morphs);

        public static MorphSet Create(List<string> morphs) => TakeArray(morphs.ToArray());

        public string Join(string seperator) => string.Join(seperator, morphs);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public FastArrayEnumerator<string> GetEnumerator() => new FastArrayEnumerator<string>(morphs);

        IEnumerator<string> IEnumerable<string>.GetEnumerator() => ((IEnumerable<string>)morphs).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => morphs.GetEnumerator();
    }
}
