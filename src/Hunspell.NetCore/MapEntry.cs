using System.Collections.Generic;
using System.Linq;
using Hunspell.Infrastructure;

namespace Hunspell
{
    public sealed class MapEntry : ArrayWrapper<string>
    {
        public static readonly MapEntry Empty = TakeArray(ArrayEx<string>.Empty);

        private MapEntry(string[] values)
            : base(values)
        {
        }

        internal static MapEntry TakeArray(string[] values) => values == null ? Empty : new MapEntry(values);

        public static MapEntry Create(List<string> values) => values == null ? Empty : TakeArray(values.ToArray());

        public static MapEntry Create(IEnumerable<string> values) => values == null ? Empty : TakeArray(values.ToArray());
    }
}
