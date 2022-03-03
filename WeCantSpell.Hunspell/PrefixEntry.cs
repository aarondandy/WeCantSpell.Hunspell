using System;

namespace WeCantSpell.Hunspell;

public sealed class PrefixEntry : AffixEntry
{
    public PrefixEntry(
        string strip,
        string affixText,
        CharacterConditionGroup conditions,
        MorphSet morph,
        FlagSet contClass)
        : base(strip, affixText, conditions, morph, contClass)
    {
    }

    public sealed override string Key => Append;

    internal bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsStartingMatch(word);
}
