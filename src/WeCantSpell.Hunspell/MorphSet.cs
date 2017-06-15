using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class MorphSet : ArrayWrapper<string>
    {
        public static readonly MorphSet Empty = TakeArray(ArrayEx<string>.Empty);

        public static readonly ArrayWrapperComparer<string, MorphSet> DefaultComparer = new ArrayWrapperComparer<string, MorphSet>();

        private MorphSet(string[] morphs)
            : base(morphs)
        {
        }

        internal static MorphSet TakeArray(string[] morphs) => morphs == null ? Empty : new MorphSet(morphs);

        public static MorphSet Create(List<string> morphs) => morphs == null ? Empty : TakeArray(morphs.ToArray());

        public static MorphSet Create(IEnumerable<string> morphs) => morphs == null ? Empty : TakeArray(morphs.ToArray());

        public string Join(string seperator) => string.Join(seperator, items);

        public bool AnyStartsWith(string text) =>
            AnyStartsWith(items, text);

        internal static bool AnyStartsWith(string[] morphs, string text)
        {
            if (morphs != null && !string.IsNullOrEmpty(text))
            {
                foreach(var morph in morphs)
                {
                    if (morph != null && morph.StartsWith(text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static string[] CreateReversed(string[] oldMorphs)
        {
            var newMorphs = new string[oldMorphs.Length];
            var lastIndex = oldMorphs.Length - 1;
            for (int i = 0; i < oldMorphs.Length; i++)
            {
                newMorphs[i] = oldMorphs[lastIndex - i].Reverse();
            }

            return newMorphs;
        }
    }
}
