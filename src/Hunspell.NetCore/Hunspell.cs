using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public static Hunspell FromFile(string dictionaryFilePath)
        {
            var dictionary = DictionaryReader.ReadFile(dictionaryFilePath);
            return new Hunspell(dictionary);
        }

        public static async Task<Hunspell> FromFileAsync(string dictionaryFilePath)
        {
            var dictionary = await DictionaryReader.ReadFileAsync(dictionaryFilePath);
            return new Hunspell(dictionary);
        }

        public bool Check(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).Check();
        }

        public SpellCheckResult CheckDetails(string word)
        {
            return new HunspellQueryState(word, Affix, Dictionary).CheckDetails();
        }

        public List<string> Suggest(string givenWord)
        {
            throw new NotImplementedException();
        }
    }
}
