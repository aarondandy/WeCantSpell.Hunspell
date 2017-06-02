using WeCantSpell.Hunspell.Infrastructure;

#if !NO_INLINE
using System.Runtime.CompilerServices;
#endif

namespace WeCantSpell.Hunspell
{
    public enum CapitalizationType : byte
    {
        /// <summary>
        /// No letters capitalized.
        /// </summary>
        None = 0,

        /// <summary>
        /// Initial letter capitalized.
        /// </summary>
        Init = 1,

        /// <summary>
        /// All letters capitalized.
        /// </summary>
        All = 2,

        /// <summary>
        /// Mixed case.
        /// </summary>
        Huh = 3,

        /// <summary>
        /// Initial letter capitalized with mixed case.
        /// </summary>
        HuhInit = 4
    }

    public static class CapitalizationTypeEx
    {
        public static CapitalizationType GetCapitalizationType(string word, AffixConfig affix) =>
            GetCapitalizationType(StringSlice.Create(word), affix);

        internal static CapitalizationType GetCapitalizationType(StringSlice word, AffixConfig affix)
        {
            if (word.IsNullOrEmpty)
            {
                return CapitalizationType.None;
            }

            var hasFoundMoreCaps = false;
            var firstIsUpper = false;
            var hasLower = false;
            var c = word.Text[word.Offset];
            if (char.IsUpper(c))
            {
                firstIsUpper = true;
            }
            else if (!CharIsNeutral(c, affix))
            {
                hasLower = true;
            }

            var wordIndexLimit = word.Length + word.Offset;
            for (int i = word.Offset + 1; i < wordIndexLimit; i++)
            {
                c = word.Text[i];

                if (char.IsUpper(c))
                {
                    hasFoundMoreCaps = true;
                }
                else if (!CharIsNeutral(c, affix))
                {
                    hasLower = true;
                }

                if (hasLower && hasFoundMoreCaps)
                {
                    break;
                }
            }

            if (firstIsUpper)
            {
                if (!hasFoundMoreCaps)
                {
                    return CapitalizationType.Init;
                }
                if (!hasLower)
                {
                    return CapitalizationType.All;
                }

                return CapitalizationType.HuhInit;
            }
            else
            {
                if (!hasFoundMoreCaps)
                {
                    return CapitalizationType.None;
                }
                if (!hasLower)
                {
                    return CapitalizationType.All;
                }

                return CapitalizationType.Huh;
            }
        }

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool CharIsNeutral(char c, AffixConfig affix) =>
            !char.IsLower(c) || (c > 127 && affix.Culture.TextInfo.ToUpper(c) == c);
    }
}
