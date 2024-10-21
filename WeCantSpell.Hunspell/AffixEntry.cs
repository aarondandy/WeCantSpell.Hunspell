using System;
using System.Diagnostics;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Key = {Key}, Conditions = {Conditions}")]
public abstract class AffixEntry
{
    protected AffixEntry(
        string strip,
        string affixText,
        CharacterConditionGroup conditions,
        MorphSet morph,
        FlagSet contClass,
        FlagValue aFlag,
        AffixEntryOptions options)
    {
        Strip = strip ?? string.Empty;
        Append = affixText ?? string.Empty;
        Conditions = conditions;
        MorphCode = morph;
        ContClass = contClass;
        AFlag = aFlag;
        Options = options;
    }

    /// <summary>
    /// Optional morphological fields separated by spaces or tabulators.
    /// </summary>
    public MorphSet MorphCode { get; }

    /// <summary>
    /// Text matching conditions that are to be met.
    /// </summary>
    public CharacterConditionGroup Conditions { get; }

    public FlagSet ContClass { get; }

    public FlagValue AFlag { get; }

    public AffixEntryOptions Options { get; }

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

    public abstract string Key { get; }

    public bool IsKeySubset(string s2) => s2 is not null && IsKeySubset(s2.AsSpan());

    public abstract bool IsKeySubset(ReadOnlySpan<char> s2);

    public abstract bool IsWordSubset(ReadOnlySpan<char> word);

    internal bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsStartingMatch(word);

    public bool ContainsContClass(FlagValue flag) => ContClass.Contains(flag);

    public bool ContainsAnyContClass(FlagSet flags) => ContClass.ContainsAny(flags);
}

[DebuggerDisplay("Key = {Key}, Conditions = {Conditions}")]
public sealed class PrefixEntry : AffixEntry
{
    public PrefixEntry(
        string strip,
        string affixText,
        CharacterConditionGroup conditions,
        MorphSet morph,
        FlagSet contClass,
        FlagValue aFlag,
        AffixEntryOptions options)
        : base (strip, affixText, conditions, morph, contClass, aFlag, options)
    {
    }

    public override string Key => Append;

    public override bool IsKeySubset(ReadOnlySpan<char> s2) => HunspellTextFunctions.IsSubset(Key, s2);

    public override bool IsWordSubset(ReadOnlySpan<char> s2) => HunspellTextFunctions.IsSubset(Key, s2);
}

[DebuggerDisplay("Key = {Key}, Conditions = {Conditions}")]
public sealed class SuffixEntry : AffixEntry
{
    public SuffixEntry(
        string strip,
        string affixText,
        CharacterConditionGroup conditions,
        MorphSet morph,
        FlagSet contClass,
        FlagValue aFlag,
        AffixEntryOptions options)
        : base(strip, affixText, conditions, morph, contClass, aFlag, options)
    {
        Key = Append.GetReversed();
    }

    public override string Key { get; }

    public override bool IsKeySubset(ReadOnlySpan<char> s2) => HunspellTextFunctions.IsSubset(Key, s2);

    public override bool IsWordSubset(ReadOnlySpan<char> s2)
    {
        return Append.Length <= s2.Length && check(Append.AsSpan(), s2.Slice(s2.Length - Append.Length));

        static bool check(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2)
        {
            for (var i = 0; i < s1.Length; i++)
            {
                var c = s1[i];
                if (c != '.' && s2[i] != c)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
