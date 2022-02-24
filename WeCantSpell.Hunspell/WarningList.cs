using System;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class WarningList : ArrayWrapper<string>
{
    public static WarningList Empty { get; } = TakeArray(Array.Empty<string>());

    public static WarningList Create(IEnumerable<string> warnings) => warnings is null ? Empty : TakeArray(warnings.ToArray());

    internal static WarningList TakeArray(string[] warnings) => new(warnings);

    private WarningList(string[] warnings) : base(warnings, canStealArray: true)
    {
    }
}
