using System.Runtime.CompilerServices;

namespace Hunspell
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
        public static CapitalizationType GetCapitalizationType(string word, AffixConfig affix)
        {
            if (string.IsNullOrEmpty(word))
            {
                return CapitalizationType.None;
            }

            var hasFoundMoreCaps = false;
            var firstIsUpper = false;
            var hasLower = false;
            var c = word[0];
            if (char.IsUpper(c))
            {
                firstIsUpper = true;
            }
            else if (!CharIsNeutral(c, affix))
            {
                hasLower = true;
            }

            for (int i = 1; i < word.Length; i++)
            {
                c = word[i];

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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool CharIsNeutral(char c, AffixConfig affix) =>
            !char.IsLower(c) || (c > 127 && affix.Culture.TextInfo.ToUpper(c) == c);
    }
}
