using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class SingleReplacementSet : ListWrapper<SingleReplacement>
    {
        public static readonly SingleReplacementSet Empty = TakeList(new List<SingleReplacement>(0));

        public static SingleReplacementSet Create(IEnumerable<SingleReplacement> replacements) =>
            replacements == null ? Empty : TakeList(replacements.ToList());

        internal static SingleReplacementSet TakeList(List<SingleReplacement> replacements) =>
            replacements == null ? Empty : new SingleReplacementSet(replacements);

        private SingleReplacementSet(List<SingleReplacement> replacements)
            : base(replacements)
        {
        }
    }
}
