using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
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

        public bool AnyStartsWith(string text)
        {
            for (var i = 0; i < items.Length; i++)
            {
                if (items[i].StartsWith(text))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
