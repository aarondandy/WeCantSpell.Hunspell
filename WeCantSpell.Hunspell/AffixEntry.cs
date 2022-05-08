using System;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public interface IAffixEntry
{
    FlagValue AFlag { get; }
    AffixEntryOptions Options { get; }
    FlagSet ContClass { get; }
    string Append { get; }
    string Key { get; }
}

public class PrefixEntry : IAffixEntry
{
    public PrefixEntry(
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

    /// <summary>
    /// The affix string to add.
    /// </summary>
    /// <remarks>
    /// Affix (optionally with flags of continuation classes, separated by a slash).
    /// </remarks>
    public string Append { get; }

    public string Key => Append;

    /// <summary>
    /// String to strip before adding affix.
    /// </summary>
    /// <remarks>
    /// Stripping characters from beginning (at prefix rules) or
    /// end(at suffix rules) of the word.
    /// </remarks>
    public string Strip { get; }

    public FlagValue AFlag { get; }

    public AffixEntryOptions Options { get; }

    internal bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsStartingMatch(word);

    public bool ContainsContClass(FlagValue flag) => ContClass.Contains(flag);

    public bool ContainsAnyContClass(FlagValue a, FlagValue b) => ContClass.ContainsAny(a, b);

    public bool ContainsAnyContClass(FlagValue a, FlagValue b, FlagValue c) => ContClass.ContainsAny(a, b, c);

    public bool IsSubset(string s2) => IsSubset(s2.AsSpan());

    public bool IsSubset(ReadOnlySpan<char> s2) => HunspellTextFunctions.IsSubset(Key, s2);

    public bool IsExactSubset(ReadOnlySpan<char> s2) => s2.StartsWith(Key, StringComparison.Ordinal);
}

public class SuffixEntry : IAffixEntry
{
    public SuffixEntry(
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
        Key = Append.GetReversed();
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

    /// <summary>
    /// String to strip before adding affix.
    /// </summary>
    /// <remarks>
    /// Stripping characters from beginning (at prefix rules) or
    /// end(at suffix rules) of the word.
    /// </remarks>
    public string Strip { get; }

    public FlagSet ContClass { get; }

    /// <summary>
    /// The affix string to add.
    /// </summary>
    /// <remarks>
    /// Affix (optionally with flags of continuation classes, separated by a slash).
    /// </remarks>
    public string Append { get; }

    public string Key { get; }

    public FlagValue AFlag { get; }

    public AffixEntryOptions Options { get; }

    internal bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsEndingMatch(word);

    public bool ContainsContClass(FlagValue flag) => ContClass.Contains(flag);

    public bool ContainsAnyContClass(FlagValue a, FlagValue b) => ContClass.ContainsAny(a, b);

    public bool ContainsAnyContClass(FlagValue a, FlagValue b, FlagValue c) => ContClass.ContainsAny(a, b, c);

    public bool IsSubset(string s2) => IsSubset(s2.AsSpan());

    public bool IsSubset(ReadOnlySpan<char> s2) => HunspellTextFunctions.IsSubset(Key, s2);

    public bool IsReverseSubset(ReadOnlySpan<char> s2)
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

    public bool IsExactReverseSubset(ReadOnlySpan<char> s2) => s2.EndsWith(Append, StringComparison.Ordinal);
}
