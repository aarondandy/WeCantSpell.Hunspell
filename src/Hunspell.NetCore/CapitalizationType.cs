using System.Globalization;

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

    public static class CapitalizationTypeUtilities
    {
        public static CapitalizationType GetCapitalizationType(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return CapitalizationType.None;
            }

            var numberCapitalized = 0;
            var numberNeutral = 0;
            UnicodeCategory category;
            for (int i = 0; i < word.Length; i++)
            {
                category = CharUnicodeInfo.GetUnicodeCategory(word, i);
                if (category == UnicodeCategory.UppercaseLetter || category == UnicodeCategory.TitlecaseLetter)
                {
                    numberCapitalized++;
                }
                else if (
                    category != UnicodeCategory.LowercaseLetter
                    && category != UnicodeCategory.OtherLetter
                    && category != UnicodeCategory.ModifierLetter
                )
                {
                    numberNeutral++;
                }
            }

            if (numberCapitalized == 0)
            {
                return CapitalizationType.None;
            }

            category = CharUnicodeInfo.GetUnicodeCategory(word, 0);
            var firstIsCapitalized = category == UnicodeCategory.UppercaseLetter || category == UnicodeCategory.TitlecaseLetter;

            if (numberCapitalized == 1 && firstIsCapitalized)
            {
                return CapitalizationType.Init;
            }
            if (numberCapitalized == word.Length || (numberCapitalized + numberNeutral) == word.Length)
            {
                return CapitalizationType.All;
            }
            if (numberCapitalized > 1 && firstIsCapitalized)
            {
                return CapitalizationType.HuhInit;
            }

            return CapitalizationType.Huh;
        }
    }
}
