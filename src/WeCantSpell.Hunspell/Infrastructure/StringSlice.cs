using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal struct StringSlice :
        IEquatable<string>,
        IEquatable<StringSlice>
    {
        public static readonly StringSlice Null = new StringSlice
        {
            Text = null,
            Offset = 0,
            Length = 0
        };

        public static readonly StringSlice Empty = new StringSlice
        {
            Text = string.Empty,
            Offset = 0,
            Length = 0
        };

        public string Text;
        public int Offset;
        public int Length;

        public bool IsNullOrEmpty
        {
            get
            {
                return Text == null || Length == 0;
            }
        }

        public static StringSlice Create(string text)
        {
            if (text == null)
            {
                return Null;
            }
            else if (text.Length == 0)
            {
                return Empty;
            }

            return new StringSlice
            {
                Text = text,
                Offset = 0,
                Length = text.Length
            };
        }

        public override string ToString()
        {
            if (Text == null)
            {
                return null;
            }

            return Length == 0 ? string.Empty : Text.Substring(Offset, Length);
        }

        public StringSlice[] SplitOnComma()
        {
            var parts = new List<StringSlice>();

            int startIndex = Offset;
            int commaIndex;
            int partLength;
            while ((commaIndex = IndexOfRawLimited(',', startIndex)) >= 0)
            {
                partLength = commaIndex - startIndex;
                if (partLength > 0)
                {
                    parts.Add(new StringSlice
                    {
                        Text = Text,
                        Offset = startIndex,
                        Length = partLength
                    });
                }

                startIndex = commaIndex + 1;
            }

            commaIndex = Offset + Length;
            partLength = commaIndex - startIndex;
            if (partLength > 0)
            {
                parts.Add(new StringSlice
                {
                    Text = Text,
                    Offset = startIndex,
                    Length = partLength
                });
            }

            return parts.ToArray();
        }

        private int IndexOfRawLimited(char c, int rawStartIndex)
        {
            var index = Text.IndexOf(c, rawStartIndex);
            return index >= Offset + Length ? -1 : index;
        }

        public int IndexOf(string value, StringComparison comparisonType)
        {
            var rawIndex = Text.IndexOf(value, Offset, comparisonType);
            if (rawIndex < 0 || rawIndex > Offset + Length)
            {
                return -1;
            }

            return rawIndex - Offset;
        }

        public int IndexOf(string value, int startIndex, StringComparison comparisonType)
        {
            var rawIndex = Text.IndexOf(value, Offset + startIndex, comparisonType);
            if (rawIndex < 0 || rawIndex > Offset + Length)
            {
                return -1;
            }

            return rawIndex - Offset;
        }

        public int IndexOf(char c)
        {
            var rawIndex = Text.IndexOf(c, Offset);
            if (rawIndex < 0 || rawIndex >= Offset + Length)
            {
                return -1;
            }

            return rawIndex - Offset;
        }

        public string Substring(int startIndex) =>
            Text.Substring(Offset + startIndex, Length - startIndex);

        public string Substring(int startIndex, int length) =>
            Text.Substring(Offset + startIndex, length);

        public bool Equals(string other)
        {
            if (other == null)
            {
                return Text == null;
            }

            return Length == other.Length
                && StringEx.EqualsOffset(Text, Offset, other, 0, Length);
        }

        public bool Equals(StringSlice other) =>
            Length == other.Length
                && StringEx.EqualsOffset(Text, Offset, other.Text, other.Offset, Length);

        public override bool Equals(object obj)
        {
            if (obj is StringSlice)
            {
                return Equals((StringSlice)obj);
            }

            var stringValue = obj as string;
            if (stringValue != null)
            {
                return Equals(stringValue);
            }

            return false;
        }

        public override int GetHashCode() => 0;

        public char[] ToCharArray() => Text.ToCharArray(Offset, Length);

        public StringSlice Subslice(int startIndex)
        {
            var localStartOffset = Offset + startIndex;

            return new StringSlice
            {
                Text = Text,
                Offset = Offset + startIndex,
                Length = Length - startIndex
            };
        }

        public StringSlice Subslice(int startIndex, int length)
        {
            return new StringSlice
            {
                Text = Text,
                Offset = Offset + startIndex,
                Length = length
            };
        }
    }
}
