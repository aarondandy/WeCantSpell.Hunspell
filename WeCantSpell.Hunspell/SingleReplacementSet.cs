using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class SingleReplacementSet : ArrayWrapper<SingleReplacement>
{
    public static readonly SingleReplacementSet Empty = TakeArray(ArrayEx<SingleReplacement>.Empty);

    public static SingleReplacementSet Create(IEnumerable<SingleReplacement> replacements) => replacements is null ? Empty : TakeArray(replacements.ToArray());

    internal static SingleReplacementSet TakeArray(SingleReplacement[] replacements) => replacements is null ? Empty : new SingleReplacementSet(replacements);

    private SingleReplacementSet(SingleReplacement[] replacements) : base(replacements)
    {
    }
}
