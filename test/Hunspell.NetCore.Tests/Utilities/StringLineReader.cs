using System.Linq;
using System.Threading.Tasks;

namespace Hunspell.NetCore.Tests.Utilities
{
    public class StringLineReader : IAffixFileLineReader
    {
        private static readonly char[] LineBreakChars = new[]{'\n','\r'};

        private int position = 0;

        private string Content { get; }

        public StringLineReader(string text)
        {
            Content = text;
        }

        public Task<string> ReadLineAsync()
        {
            string result;
            if (Content == null || position >= Content.Length)
            {
                result = null;
            }
            else
            {
                var startPosition = position;
                position = Content.IndexOfAny(LineBreakChars, position);
                if (position < 0)
                {
                    position = Content.Length;
                }

                result = Content.Substring(startPosition, position - startPosition);
                for(;position < Content.Length && LineBreakChars.Contains(Content[position]); position++)
                {
                    ;
                }
            }

            return Task.FromResult(result);
        }
    }
}
