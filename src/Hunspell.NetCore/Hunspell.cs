using System;

namespace Hunspell
{
    public class Hunspell
    {
        public Hunspell(Dictionary dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            Dictionary = dictionary;
        }

        public Dictionary Dictionary { get; }

        public AffixConfig Affix => Dictionary.Affix;

        public bool Check(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).Check();
        }

        public SpellCheckResult CheckDetails(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).CheckDetails();
        }
    }
}
