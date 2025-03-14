﻿using System;
using System.Diagnostics;

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

    public abstract bool IsKeySubset(string s2);

    public abstract bool IsKeySubset(ReadOnlySpan<char> s2);

    public abstract bool IsWordSubset(string word);

    public abstract bool IsWordSubset(ReadOnlySpan<char> word);

    internal abstract bool TestCondition(ReadOnlySpan<char> word);

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

    public override bool IsKeySubset(string s2) => StringEx.IsSubset(Append, s2);

    public override bool IsKeySubset(ReadOnlySpan<char> s2) => StringEx.IsSubset(Append, s2);

    public override bool IsWordSubset(string word) => StringEx.IsSubset(Append, word);

    public override bool IsWordSubset(ReadOnlySpan<char> word) => StringEx.IsSubset(Append, word);

    internal override bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsStartingMatch(word);
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
        _key = Append.GetReversed();
    }

    private readonly string _key;

    public override string Key => _key;

    public override bool IsKeySubset(string s2) => s2 is not null && StringEx.IsSubset(_key, s2);

    public override bool IsKeySubset(ReadOnlySpan<char> s2) => StringEx.IsSubset(_key, s2);

    public override bool IsWordSubset(string word)
    {
        return word is not null
            && Append.Length <= word.Length
            && StringEx.IsSubset(Append, word.AsSpan(word.Length - Append.Length));
    }

    public override bool IsWordSubset(ReadOnlySpan<char> word)
    {
        return Append.Length <= word.Length
            && StringEx.IsSubset(Append, word.Slice(word.Length - Append.Length));
    }

    internal override bool TestCondition(ReadOnlySpan<char> word) => Conditions.IsEndingMatch(word);
}
