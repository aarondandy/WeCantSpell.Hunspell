using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class CompoundRule : ArrayWrapper<FlagValue>
    {
        public static readonly CompoundRule Empty = TakeArray(ArrayEx<FlagValue>.Empty);

        private CompoundRule(FlagValue[] values)
            : base(values)
        {
        }

        internal static CompoundRule TakeArray(FlagValue[] values) => values == null ? Empty : new CompoundRule(values);

        public static CompoundRule Create(List<FlagValue> values) => values == null ? Empty : TakeArray(values.ToArray());

        public static CompoundRule Create(IEnumerable<FlagValue> values) => values == null ? Empty : TakeArray(values.ToArray());
    }
}
