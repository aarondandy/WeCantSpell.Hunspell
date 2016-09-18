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
            return Text.Substring(Offset, Length);
        }

        public StringSlice[] SplitOnComma()
        {
            var temp = Text.Substring(Offset, Length).SplitOnComma();
            return ArrayEx.ConvertAll(temp, Create);
        }
    }
}
