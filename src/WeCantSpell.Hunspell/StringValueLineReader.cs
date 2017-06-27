using System.Text;
using WeCantSpell.Hunspell.Infrastructure;

#if !NO_ASYNC
using System.Threading.Tasks;
#endif

namespace WeCantSpell.Hunspell
{
    public sealed class StringValueLineReader : IHunspellLineReader
    {
        public StringValueLineReader(string text) => content = text;

        private readonly string content;

        private int position = 0;

        public Encoding CurrentEncoding => Encoding.Unicode;

        public string ReadLine()
        {
            if (content == null || position >= content.Length)
            {
                return null;
            }

            var startPosition = position;
            position = content.FirstIndexOfLineBreakChar(position);
            if (position < 0)
            {
                position = content.Length;
            }

            var result = content.Substring(startPosition, position - startPosition);

            while (position < content.Length && content[position].IsLineBreakChar())
            {
                position++;
            }

            return result;
        }

#if !NO_ASYNC
        public Task<string> ReadLineAsync() => Task.FromResult(ReadLine());
#endif

    }
}
