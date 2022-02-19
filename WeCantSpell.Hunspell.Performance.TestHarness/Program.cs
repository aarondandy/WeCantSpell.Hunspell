﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeCantSpell.Hunspell.Performance.TestHarness
{
    public class Program
    {
        static void Main(string[] args)
        {
            //DictionaryLoads();
            Checks();
            //Suggestions();
        }

        static void DictionaryLoads()
        {
            var testAssemblyPath = Path.GetFullPath(typeof(Program).Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            var dictionaryFilePaths = Directory.GetFiles(filesDirectory, "*.dic").OrderBy(p => p);

            foreach (var dictionaryFilePath in dictionaryFilePaths)
            {
                WordList.CreateFromFiles(dictionaryFilePath);
            }
        }

        static void Checks()
        {
            var hunspell = WordList.CreateFromFiles("files/English (American).dic");
            var words = ReadWords().ToList();

            for (var i = 0; i < 1000; i++)
            {
                foreach (var word in words)
                {
                    hunspell.Check(word);
                }
            }
        }

        static void Suggestions()
        {
            var hunspell = WordList.CreateFromFiles("files/English (American).dic");
            var words = ReadWords()
                .Take(500)
                .ToList();

            foreach (var word in words)
            {
                var isFound = hunspell.Check(word);
                var suggestions = hunspell.Suggest(word);
            }
        }

        private static readonly char[] CommonWordSplitChars = { ' ', '\t', ',' };

        private static IEnumerable<string> ReadWords()
        {
            return File.ReadAllLines("files/List_of_common_misspellings.txt", Encoding.UTF8)
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Trim())
                .Where(line => line.Length != 0 && !line.StartsWith("#") && !line.StartsWith("["))
                .SelectMany(line => line.Split(CommonWordSplitChars, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
