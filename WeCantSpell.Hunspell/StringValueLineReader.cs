using System.Text;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class StringValueLineReader : IHunspellLineReader
{
    public StringValueLineReader(string text)
    {
        _content = text;
    }

    private readonly string _content;

    private int _position = 0;

    public Encoding CurrentEncoding => Encoding.Unicode;

    public string ReadLine()
    {
        if (_content is null || _position >= _content.Length)
        {
            return null;
        }

        var startPosition = _position;

        for (; _position < _content.Length && !_content[_position].IsLineBreakChar(); ++_position) ;
        
        var result = _content.Substring(startPosition, _position - startPosition);

        for (; _position < _content.Length && _content[_position].IsLineBreakChar(); _position++) ;

        return result;
    }

    public Task<string> ReadLineAsync() => Task.FromResult(ReadLine());
}
