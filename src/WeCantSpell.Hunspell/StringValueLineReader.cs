using System.Text;

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class StringValueLineReader : IHunspellLineReader
    {
        private static readonly char[] LineBreakChars = new[] { '\n', '\r' };

        private static bool IsLineBreakChar(char c) => c == '\n' || c == '\r';

        public StringValueLineReader(string text) => Content = text;

        private int position = 0;

        private string Content { get; }

        public Encoding CurrentEncoding => Encoding.UTF8;

        public string ReadLine()
        {
            if (Content == null || position >= Content.Length)
            {
                return null;
            }

            var startPosition = position;
            position = Content.IndexOfAny(LineBreakChars, position);
            if (position < 0)
            {
                position = Content.Length;
            }

            var result = Content.Substring(startPosition, position - startPosition);

            for (; position < Content.Length && IsLineBreakChar(Content[position]); position++) ;

            return result;
        }

#if !NO_ASYNC
        public Task<string> ReadLineAsync() => Task.FromResult(ReadLine());
#endif
    }
}
