using System;
using Hunspell.Infrastructure;

namespace Hunspell.Infrastructure
{
    internal struct StringSlice
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
            else if(text.Length == 0)
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
            return Length == 0 ? string.Empty : Text.Substring(Offset, Length);
        }

        public StringSlice[] SplitOnComma()
        {
            var temp = Text.Substring(Offset, Length).SplitOnComma();
            return ArrayEx.ConvertAll(temp, Create);
        }

        public int IndexOf(string value, StringComparison comparisonType)
        {
            return Text.IndexOf(value, Offset, comparisonType);
        }

        public int IndexOf(string value, int startIndex, StringComparison comparisonType)
        {
            return Text.IndexOf(value, Offset + startIndex, comparisonType);
        }

        public string Substring(int startIndex) =>
            Text.Substring(Offset + startIndex, Length - startIndex);

        public string Substring(int startIndex, int length) =>
            Text.Substring(Offset + startIndex, length);
    }
}
