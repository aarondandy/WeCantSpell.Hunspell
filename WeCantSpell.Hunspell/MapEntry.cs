﻿using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell
{
    public sealed class MapEntry : ArrayWrapper<string>
    {
        public static readonly MapEntry Empty = TakeArray(ArrayEx<string>.Empty);

        public static MapEntry Create(IEnumerable<string> values) => values == null ? Empty : TakeArray(values.ToArray());

        internal static MapEntry TakeArray(string[] values) => values == null ? Empty : new MapEntry(values);

        private MapEntry(string[] values)
            : base(values)
        {
        }
    }
}
