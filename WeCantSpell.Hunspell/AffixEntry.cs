﻿#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

using System;

namespace WeCantSpell.Hunspell
{
    /// <summary>
    /// An affix is either a prefix or a suffix attached to root words to make other words.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Basically a Prefix or a Suffix is set of <see cref="AffixEntry"/> objects
    /// which store information about the prefix or suffix along
    /// with supporting routines to check if a word has a particular
    /// prefix or suffix or a combination.
    /// </para>
    /// 
    /// Appendix:  Understanding Affix Code
    /// 
    /// <para>
    /// An affix is either a  prefix or a suffix attached to root words to make 
    /// other words.
    /// </para>
    /// 
    /// <para>
    /// Basically a prefix or a suffix is set of <see cref="AffixEntry"/> objects
    /// which store information about the prefix or suffix along
    /// with supporting routines to check if a word has a particular
    /// prefix or suffix or a combination.
    /// </para>
    /// 
    /// <para>
    /// Zero stripping or affix are indicated by zero. Zero condition is indicated by dot.
    /// Condition is a simplified, regular expression-like pattern, which must be met
    /// before the affix can be applied. (Dot signs an arbitrary character.Characters in braces
    /// sign an arbitrary character from the character subset.Dash hasn't got special
    /// meaning, but circumflex(^) next the first brace sets the complementer character set.)
    /// </para>
    /// 
    /// </remarks>
    /// 
    /// <example>
    /// 
    /// <para>
    /// Here is a suffix borrowed from the en_US.aff file.  This file 
    /// is whitespace delimited.
    /// </para>
    /// 
    /// <code>
    /// SFX D Y 4 
    /// SFX D   0     e          d
    /// SFX D   y     ied        [^aeiou]y
    /// SFX D   0     ed         [^ey]
    /// SFX D   0     ed         [aeiou]y
    /// </code>
    /// 
    /// This information can be interpreted as follows
    /// 
    /// <para>
    /// In the first line has 4 fields which define the <see cref="AffixEntryGroup{TEntry}"/> for this affix that will contain four <see cref="SuffixEntry"/> objects.
    /// </para>
    /// 
    /// <code>
    /// Field
    /// -----
    /// 1     SFX - indicates this is a suffix
    /// 2     D   - is the name of the character flag which represents this suffix
    /// 3     Y   - indicates it can be combined with prefixes (cross product)
    /// 4     4   - indicates that sequence of 4 affentry structures are needed to
    ///                properly store the affix information
    /// </code>
    /// 
    /// <para>
    /// The remaining lines describe the unique information for the 4 <see cref="SuffixEntry"/> 
    /// objects that make up this affix.  Each line can be interpreted
    /// as follows: (note fields 1 and 2 are as a check against line 1 info)
    /// </para>
    /// 
    /// <code>
    /// Field
    /// -----
    /// 1     SFX         - indicates this is a suffix
    /// 2     D           - is the name of the character flag for this affix
    /// 3     y           - the string of chars to strip off before adding affix
    ///                          (a 0 here indicates the NULL string)
    /// 4     ied         - the string of affix characters to add
    /// 5     [^aeiou]y   - the conditions which must be met before the affix
    ///                     can be applied
    /// </code>
    /// 
    /// <para>
    /// Field 5 is interesting.  Since this is a suffix, field 5 tells us that
    /// there are 2 conditions that must be met.  The first condition is that 
    /// the next to the last character in the word must *NOT* be any of the 
    /// following "a", "e", "i", "o" or "u".  The second condition is that
    /// the last character of the word must end in "y".
    /// </para>
    /// 
    /// </example>
    /// 
    /// <seealso cref="PrefixEntry"/>
    /// <seealso cref="SuffixEntry"/>
    public abstract class AffixEntry
    {
#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        protected AffixEntry(
            string strip,
            string affixText,
            CharacterConditionGroup conditions,
            MorphSet morph,
            FlagSet contClass)
        {
            Strip = strip ?? string.Empty;
            Append = affixText ?? string.Empty;
            Conditions = conditions ?? CharacterConditionGroup.Empty;
            MorphCode = morph ?? MorphSet.Empty;
            ContClass = contClass ?? FlagSet.Empty;
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

        public abstract string Key { get; }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsContClass(FlagValue flag) => ContClass.Contains(flag);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyContClass(FlagValue a, FlagValue b) => ContClass.ContainsAny(a,b);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool ContainsAnyContClass(FlagValue a, FlagValue b, FlagValue c) => ContClass.ContainsAny(a, b, c);
    }
}
