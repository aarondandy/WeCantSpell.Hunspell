using System;

namespace WeCantSpell.Hunspell
{
    /// <summary>
    /// Various options that can be enabled as part of an <seealso cref="AffixConfig"/>.
    /// </summary>
    /// <seealso cref="AffixConfig"/>
    [Flags]
    public enum AffixConfigOptions : short
    {
        /// <summary>
        /// Indicates that no options are set.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates agglutinative languages with right-to-left writing system.
        /// </summary>
        /// <remarks>
        /// Set twofold prefix stripping (but single suffix stripping) eg. for morphologically complex
        /// languages with right-to-left writing system.
        /// </remarks>
        ComplexPrefixes = 1 << 0,

        /// <summary>
        /// Allow twofold suffixes within compounds.
        /// </summary>
        CompoundMoreSuffixes = 1 << 1,

        /// <summary>
        /// Forbid word duplication in compounds (e.g. foofoo).
        /// </summary>
        CheckCompoundDup = 1 << 2,

        /// <summary>
        /// Forbid compounding if the compound word may be a non compound word with a REP fault.
        /// </summary>
        /// <remarks>
        /// Forbid compounding, if the (usually bad) compound word may be
        /// a non compound word with a REP fault. Useful for languages with
        /// 'compound friendly' orthography.
        /// </remarks>
        CheckCompoundRep = 1 << 3,

        /// <summary>
        /// Forbid compounding if the compound word contains triple repeating letters.
        /// </summary>
        /// <remarks>
        /// Forbid compounding, if compound word contains triple repeating letters
        /// (e.g.foo|ox or xo|oof). Bug: missing multi-byte character support
        /// in UTF-8 encoding(works only for 7-bit ASCII characters).
        /// </remarks>
        CheckCompoundTriple = 1 << 4,

        /// <summary>
        /// Allow simplified 2-letter forms of the compounds forbidden by <see cref="CheckCompoundTriple"/>.
        /// </summary>
        /// <remarks>
        /// It's useful for Swedish and Norwegian (and for
        /// the old German orthography: Schiff|fahrt -> Schiffahrt).
        /// </remarks>
        SimplifiedTriple = 1 << 5,

        /// <summary>
        /// Forbid upper case characters at word boundaries in compounds.
        /// </summary>
        CheckCompoundCase = 1 << 6,

        /// <summary>
        /// A flag used by the controlled compound words.
        /// </summary>
        CheckNum = 1 << 7,

        /// <summary>
        /// Remove all bad n-gram suggestions (default mode keeps one).
        /// </summary>
        /// <seealso cref="AffixConfig.MaxDifferency"/>
        OnlyMaxDiff = 1 << 8,

        /// <summary>
        /// Disable word suggestions with spaces.
        /// </summary>
        NoSplitSuggestions = 1 << 9,

        /// <summary>
        /// Indicates that affix rules can strip full words.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When active, affix rules can strip full words, not only one less characters, before
        /// adding the affixes, see fullstrip.* test files in the source distribution).
        /// </para>
        /// <para>
        /// Note: conditions may be word length without <see cref="FullStrip"/>, too.
        /// </para>
        /// </remarks>
        FullStrip = 1 << 10,

        /// <summary>
        /// Add dot(s) to suggestions, if input word terminates in dot(s).
        /// </summary>
        /// <remarks>
        /// Not for LibreOffice dictionaries, because LibreOffice
        /// has an automatic dot expansion mechanism.
        /// </remarks>
        SuggestWithDots = 1 << 11,

        /// <summary>
        /// When active, words marked with the <see cref="AffixConfig.Warn"/> flag aren't accepted by the spell checker.
        /// </summary>
        /// <remarks>
        /// Words with flag <see cref="AffixConfig.Warn"/> aren't accepted by the spell checker using this parameter.
        /// </remarks>
        ForbidWarn = 1 << 12,

        /// <summary>
        /// Indicates SS letter pair in uppercased (German) words may be upper case sharp s (ß).
        /// </summary>
        /// <remarks>
        /// SS letter pair in uppercased (German) words may be upper case sharp s (ß).
        /// Hunspell can handle this special casing with the CHECKSHARPS
        /// declaration (see also <see cref="AffixConfig.KeepCase"/> flag and tests/germancompounding example)
        /// in both spelling and suggestion.
        /// </remarks>
        /// <seealso cref="AffixConfig.KeepCase"/>
        CheckSharps = 1 << 13,

        SimplifiedCompound = 1 << 14
    }
}
