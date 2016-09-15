using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class MorphSet
        : IReadOnlyList<string>
    {
        public static readonly MorphSet Empty = MorphSet.TakeArray(ArrayEx<string>.Empty);

        private readonly string[] morphs;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private MorphSet(string[] morphs)
        {
            this.morphs = morphs;
        }

        public string this[int index] => morphs[index];

        public int Count => morphs.Length;

        public bool HasMorphs => morphs.Length != 0;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static MorphSet TakeArray(string[] morphs) => new MorphSet(morphs);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static MorphSet Create(List<string> morphs) => TakeArray(morphs.ToArray());

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public string Join(string seperator) => string.Join(seperator, morphs);

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)morphs).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => morphs.GetEnumerator();
    }
}
