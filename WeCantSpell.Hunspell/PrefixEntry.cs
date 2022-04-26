using System;

namespace WeCantSpell.Hunspell;

public sealed class PrefixEntry
{
    public PrefixEntry(
        string strip,
        string affixText,
        CharacterConditionGroup conditions,
        MorphSet morph,
        FlagSet contClass)
    {
        Strip = strip ?? string.Empty;
        Append = affixText ?? string.Empty;
        Conditions = conditions;
        MorphCode = morph;
        ContClass = contClass;
    }

    /// <summary>
    /// Optional morphological fields separated by spaces or tabulators.
    /// </summary>
    public MorphSet MorphCode { get; }

    /// <summary>
    /// Text matching conditions that are to be met.
    /// </summary>
    public CharacterConditionGroup Conditions { get; }

    /// <summary>
    /// The affix string to add.
    /// </summary>
    /// <remarks>
    /// Affix (optionally with flags of continuation classes, separated by a slash).
    /// </remarks>
    public string Append { get; }

    /// <summary>
    /// String to strip before adding affix.
    /// </summary>
    /// <remarks>
    /// Stripping characters from beginning (at prefix rules) or
    /// end(at suffix rules) of the word.
    /// </remarks>
    public string Strip { get; }

    public FlagSet ContClass { get; }

    public string Key => Append;

    internal bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsStartingMatch(word);

    public bool ContainsContClass(FlagValue flag) => ContClass.Contains(flag);

    public bool ContainsAnyContClass(FlagValue a, FlagValue b) => ContClass.ContainsAny(a, b);

    public bool ContainsAnyContClass(FlagValue a, FlagValue b, FlagValue c) => ContClass.ContainsAny(a, b, c);
}
