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

            int numberCapitalized = 0;
            int numberNeutral = 0;
            bool hasLower = false;
            var c = word[0];
            if (char.IsUpper(c))
            {
                numberCapitalized = 1;

                for (int i = 1; i < word.Length; i++)
                {
                    c = word[i];

                    if (char.IsUpper(c))
                    {
                        numberCapitalized++;
                    }
                    else if (CharIsNeutral(c, affix))
                    {
                        numberNeutral++;
                    }
                    else
                    {
                        hasLower = true;
                    }

                    if (hasLower && numberCapitalized > 1)
                    {
                        break;
                    }
                }

                if (numberCapitalized == 1)
                {
                    return CapitalizationType.Init;
                }
                if (numberCapitalized == word.Length || (numberCapitalized + numberNeutral) == word.Length)
                {
                    return CapitalizationType.All;
                }

                return CapitalizationType.HuhInit;
            }
            else
            {
                if (CharIsNeutral(c, affix))
                {
                    numberNeutral = 1;
                }
                else
                {
                    hasLower = true;
                }

                for (int i = 1; i < word.Length; i++)
                {
                    c = word[i];

                    if (char.IsUpper(c))
                    {
                        numberCapitalized++;
                    }
                    else if (CharIsNeutral(c, affix))
                    {
                        numberNeutral++;
                    }
                    else
                    {
                        hasLower = true;
                    }

                    if (hasLower && numberCapitalized != 0)
                    {
                        break;
                    }
                }

                if (numberCapitalized == 0)
                {
                    return CapitalizationType.None;
                }
                if (numberCapitalized == word.Length || (numberCapitalized + numberNeutral) == word.Length)
                {
                    return CapitalizationType.All;
                }

                return CapitalizationType.Huh;
            }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool CharIsNeutral(char c, AffixConfig affix)
        {
            return !char.IsLower(c) || (c > 127 && affix.Culture.TextInfo.ToUpper(c) == c);
        }
    }
}
