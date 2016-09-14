using System.Collections;
using System.Collections.Generic;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class MorphSet
        : IReadOnlyList<string>
    {
        public static readonly MorphSet Empty = MorphSet.TakeArray(ArrayEx<string>.Empty);

        private readonly string[] morphs;

        private MorphSet(string[] morphs)
        {
            this.morphs = morphs;
        }

        public string this[int index] => morphs[index];

        public int Count => morphs.Length;

        public bool HasMorphs => morphs.Length != 0;

        public static MorphSet TakeArray(string[] morphs)
        {
            return new MorphSet(morphs);
        }

        public static MorphSet Create(List<string> morphs)
        {
            return TakeArray(morphs.ToArray());
        }

        public string Join(string seperator)
        {
            return string.Join(seperator, morphs);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)morphs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return morphs.GetEnumerator();
        }
    }
}
