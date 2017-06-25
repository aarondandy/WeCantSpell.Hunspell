using System;
using System.Globalization;
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

    // TODO: bring this internal
    public static class CapitalizationTypeEx
    {
        [Obsolete]
        public static CapitalizationType GetCapitalizationType(string word, TextInfo textInfo) =>
            string.IsNullOrEmpty(word)
                ? CapitalizationType.None
                : GetCapitalizationType(new StringSlice(word), textInfo);

        internal static CapitalizationType GetCapitalizationType(StringSlice word, TextInfo textInfo)
        {
            if (word.IsEmpty)
            {
                return CapitalizationType.None;
            }

            var hasFoundMoreCaps = false;
            var firstIsUpper = false;
            var hasLower = false;
            var c = word.First();
            if (char.IsUpper(c))
            {
                firstIsUpper = true;
            }
            else if (HunspellTextFunctions.CharIsNotNeutral(c, textInfo))
            {
                hasLower = true;
            }

            var wordIndexLimit = word.Length + word.Offset;
            for (int i = word.Offset + 1; i < wordIndexLimit; i++)
            {
                c = word.Text[i];

                if (!hasFoundMoreCaps && char.IsUpper(c))
                {
                    hasFoundMoreCaps = true;
                    if (hasLower)
                    {
                        break;
                    }
                }
                else if (!hasLower && HunspellTextFunctions.CharIsNotNeutral(c, textInfo))
                {
                    hasLower = true;
                    if (hasFoundMoreCaps)
                    {
                        break;
                    }
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
    }
}
