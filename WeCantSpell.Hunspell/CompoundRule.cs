using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class CompoundRule : ArrayWrapper<FlagValue>
    {
        public static readonly CompoundRule Empty = TakeArray(ArrayEx<FlagValue>.Empty);

        public static CompoundRule Create(List<FlagValue> values) => values == null ? Empty : TakeArray(values.ToArray());

        public static CompoundRule Create(IEnumerable<FlagValue> values) => values == null ? Empty : TakeArray(values.ToArray());

        internal static CompoundRule TakeArray(FlagValue[] values) => values == null ? Empty : new CompoundRule(values);

        private CompoundRule(FlagValue[] values)
            : base(values)
        {
        }

        public bool IsWildcard(int index)
        {
            var value = this[index];
            return value == '*' || value == '?';
        }

        internal bool ContainsRuleFlagForEntry(WordEntryDetail details)
        {
            foreach (var flag in Items)
            {
                if (!flag.IsWildcard && details.ContainsFlag(flag))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
