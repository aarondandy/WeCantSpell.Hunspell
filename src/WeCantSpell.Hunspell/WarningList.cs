using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class WarningList : ListWrapper<string>
    {
        public static WarningList Create(IEnumerable<string> warnings) =>
            warnings == null ? TakeList(null) : TakeList(warnings.ToList());

        internal static WarningList TakeList(List<string> warnings) =>
            new WarningList(warnings ?? new List<string>(0));

        private WarningList(List<string> warnings)
            : base(warnings)
        {
        }
    }
}
