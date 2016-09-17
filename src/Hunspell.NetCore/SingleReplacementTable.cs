using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class SingleReplacementTable : ListWrapper<SingleReplacementEntry>
    {
        public static readonly SingleReplacementTable Empty = TakeList(new List<SingleReplacementEntry>(0));

        private SingleReplacementTable(List<SingleReplacementEntry> replacements)
            : base(replacements)
        {
        }

        internal static SingleReplacementTable TakeList(List<SingleReplacementEntry> replacements) => replacements == null ? Empty : new SingleReplacementTable(replacements);

        public static SingleReplacementTable Create(IEnumerable<SingleReplacementEntry> replacements) => replacements == null ? Empty : TakeList(replacements.ToList());
    }
}
