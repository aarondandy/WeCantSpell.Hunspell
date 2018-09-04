using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class MorphSet : ArrayWrapper<string>, IEquatable<MorphSet>
    {
        public static readonly MorphSet Empty = TakeArray(ArrayEx<string>.Empty);

        public static readonly ArrayWrapperComparer<string, MorphSet> DefaultComparer = new ArrayWrapperComparer<string, MorphSet>();

        public static MorphSet Create(IEnumerable<string> morphs) => morphs == null ? Empty : TakeArray(morphs.ToArray());

        internal static MorphSet TakeArray(string[] morphs) => morphs == null ? Empty : new MorphSet(morphs);

        internal static string[] CreateReversed(string[] oldMorphs)
        {
            var newMorphs = new string[oldMorphs.Length];
            var lastIndex = oldMorphs.Length - 1;
            for (int i = 0; i < oldMorphs.Length; i++)
            {
                newMorphs[i] = oldMorphs[lastIndex - i].GetReversed();
            }

            return newMorphs;
        }

        private MorphSet(string[] morphs)
            : base(morphs)
        {
        }

        internal string Join(string seperator) => string.Join(seperator, items);

        public bool Equals(MorphSet other) =>
            !ReferenceEquals(other, null)
            &&
            (
                ReferenceEquals(this, other)
                || ArrayComparer<string>.Default.Equals(other.items, items)
            );

        public override bool Equals(object obj) =>
            Equals(obj as MorphSet);

        public override int GetHashCode() =>
            ArrayComparer<string>.Default.GetHashCode(items);
    }
}
