using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class PhoneTable : ArrayWrapper<PhoneticEntry>
{
    public static readonly PhoneTable Empty = TakeArray(ArrayEx<PhoneticEntry>.Empty);

    public static PhoneTable Create(IEnumerable<PhoneticEntry> entries) => entries is null ? Empty : TakeArray(entries.ToArray());

    internal static PhoneTable TakeArray(PhoneticEntry[] entries) => entries is null ? Empty : new PhoneTable(entries);

    private PhoneTable(PhoneticEntry[] entries) : base(entries)
    {
    }
}
