using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal struct StringSlice :
        IEquatable<string>,
        IEquatable<StringSlice>
    {
        public static readonly StringSlice Null = new StringSlice(null, 0, 0);

        public static readonly StringSlice Empty = new StringSlice(string.Empty, 0, 0);

        public readonly string Text;

        public readonly int Offset;

        public readonly int Length;

        public bool IsNullOrEmpty => Text == null || Length == 0;

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

            return new StringSlice(text, 0, text.Length);
        }

        public StringSlice(string text, int startIndex, int length)
        {
            Text = text;
            Offset = startIndex;
            Length = length;
        }

        public override string ToString() =>
            Text == null
            ? null
            : Length == 0
            ? string.Empty
            : Offset == 0 && Length == Text.Length
            ? Text
            : Text.Substring(Offset, Length);

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
                    parts.Add(new StringSlice(Text, startIndex, partLength));
                }

                startIndex = commaIndex + 1;
            }

            commaIndex = Offset + Length;
            partLength = commaIndex - startIndex;
            if (partLength > 0)
            {
                parts.Add(new StringSlice(Text, startIndex, partLength));
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
            if (obj is StringSlice slice)
            {
                return Equals(slice);
            }
            if (obj is string stringValue)
            {
                return Equals(stringValue);
            }

            return false;
        }

        public override int GetHashCode() => 0;

        public char[] ToCharArray() => Text.ToCharArray(Offset, Length);

        public StringSlice Subslice(int startIndex) =>
            new StringSlice(Text, Offset + startIndex, Length - startIndex);

        public StringSlice Subslice(int startIndex, int length) =>
            new StringSlice(Text, Offset + startIndex, length);
    }
}
