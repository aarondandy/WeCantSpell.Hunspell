using Hunspell.Infrastructure;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Hunspell
{
    public struct CharacterCondition
    {
        private static Regex ConditionParsingRegex = new Regex(
            @"^(\[[^\]]*\]|\.|[^\[\]\.])*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static readonly CharacterCondition AllowAny = new CharacterCondition(CharacterSet.Empty, true);

        public CharacterCondition(CharacterSet characters, bool restricted)
        {
            Characters = characters;
            Restricted = restricted;
        }

        private CharacterCondition(char character, bool restricted)
            : this(CharacterSet.Create(character), restricted)
        {
        }

        private CharacterCondition(char[] characters, bool restricted)
            : this(CharacterSet.TakeArray(characters), restricted)
        {
        }

        public CharacterSet Characters { get; }

        /// <summary>
        /// Indicates that the <see cref="Characters"/> are restricted when <c>true</c>.
        /// </summary>
        public bool Restricted { get; }

        internal static CharacterCondition TakeArray(char[] characters, bool restricted) => new CharacterCondition(characters, restricted);

        public static CharacterCondition Create(char character, bool restricted) => new CharacterCondition(character, restricted);

        public static CharacterCondition Create(IEnumerable<char> characters, bool restricted) => TakeArray(characters.ToArray(), restricted);

        public static CharacterConditionGroup Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return CharacterConditionGroup.Empty;
            }

            var match = ConditionParsingRegex.Match(text);
            if (!match.Success || match.Groups.Count < 2)
            {
                return CharacterConditionGroup.Empty;
            }

            var captures = match.Groups[1].Captures;
            var conditions = new CharacterCondition[captures.Count];
            for (var captureIndex = 0; captureIndex < captures.Count; captureIndex++)
            {
                conditions[captureIndex] = ParseSingle(captures[captureIndex].Value);
            }

            return CharacterConditionGroup.TakeArray(conditions);
        }

        private static CharacterCondition ParseSingle(string text)
        {
            if (text.Length == 0)
            {
                throw new InvalidOperationException();
            }

            if (text.Length == 1)
            {
                var singleChar = text[0];
                if (singleChar == '.')
                {
                    return AllowAny;
                }

                return Create(singleChar, false);
            }

            if (!text.StartsWith('[') || !text.EndsWith(']'))
            {
                throw new InvalidOperationException();
            }

            if (text[1] == '^')
            {
                return TakeArray(text.ToCharArray(2, text.Length - 3), true);
            }
            else
            {
                return TakeArray(text.ToCharArray(1, text.Length - 2), false);
            }
        }

        public bool IsMatch(char c)
        {
            var isInList = Characters != null && Characters.Contains(c);

            if (Restricted)
            {
                isInList = !isInList;
            }

            return isInList;
        }

        public bool AllowsAny => Restricted && (Characters == null || Characters.Count == 0);

        public bool PermitsSingleCharacter => !Restricted && Characters != null && Characters.Count == 1;

        public string GetEncoded()
        {
            if (AllowsAny)
            {
                return ".";
            }

            if (PermitsSingleCharacter)
            {
                return Characters[0].ToString();
            }

            string lettersText;
            if (Characters == null || Characters.Count == 0)
            {
                lettersText = string.Empty;
            }
            else
            {
                lettersText = string.Concat(Characters);
            }

            return (Restricted ? "[^" : "[") + lettersText + "]";
        }

        public override string ToString() => GetEncoded();
    }
}
