using Hunspell.Utilities;
using System;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace Hunspell
{
    public struct CharacterCondition
    {
        private static Regex ConditionParsingRegex = new Regex(
            @"^(\[[^\]]*\]|\.|[^\[\]\.])*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public CharacterCondition(ImmutableSortedSet<char> characters, bool restricted)
        {
            Characters = characters;
            Restricted = restricted;
        }

        public CharacterCondition(char c, bool restricted)
            : this(ImmutableSortedSet.Create(c), restricted)
        {
        }

        public CharacterCondition(char[] characters, bool restricted)
            : this(ImmutableSortedSet.Create(characters), restricted)
        {
        }

        public static readonly CharacterCondition AllowAny = new CharacterCondition(ImmutableSortedSet<char>.Empty, true);

        public ImmutableSortedSet<char> Characters { get; }

        /// <summary>
        /// Indicates that the <see cref="Characters"/> are restricted when <c>true</c>.
        /// </summary>
        public bool Restricted { get; }

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
            var builder = ImmutableArray.CreateBuilder<CharacterCondition>(captures.Count);
            for (var captureIndex = 0; captureIndex < captures.Count; captureIndex++)
            {
                builder.Add(ParseSingle(captures[captureIndex].Value));
            }

            return new CharacterConditionGroup(builder.ToImmutable());
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

                return new CharacterCondition(singleChar, false);
            }

            if (!text.StartsWith('[') || !text.EndsWith(']'))
            {
                throw new InvalidOperationException();
            }

            if (text[1] == '^')
            {
                return new CharacterCondition(text.ToCharArray(2, text.Length - 3), true);
            }
            else
            {
                return new CharacterCondition(text.ToCharArray(1, text.Length - 2), false);
            }
        }

        public bool IsMatch(char c)
        {
            var isInList = Characters != null && Characters.Contains(c);
            if (Restricted)
            {
                return !isInList;
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

        public override string ToString()
        {
            return GetEncoded();
        }
    }
}
