using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class SuffixEntry : AffixEntry
{
    public SuffixEntry(
        string strip,
        string affixText,
        CharacterConditionGroup conditions,
        MorphSet morph,
        FlagSet contClass)
        : base(strip, affixText, conditions, morph, contClass)
    {
        Key = affixText.GetReversed();
    }

    public sealed override string Key { get; }

    internal bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsEndingMatch(word);
}
