using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class StaticEncodingLineReader : IHunspellLineReader, IDisposable
{
    public StaticEncodingLineReader(Stream stream, Encoding encoding)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _reader = new StreamReader(stream, encoding ?? Encoding.UTF8, true);
    }

    private readonly Stream _stream;
    private readonly StreamReader _reader;

    public Encoding CurrentEncoding => _reader.CurrentEncoding;

    public static List<string> ReadLines(string filePath, Encoding encoding)
    {
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));

        using var stream = FileStreamEx.OpenReadFileStream(filePath);
        using var reader = new StaticEncodingLineReader(stream, encoding);
        return reader.ReadLines().ToList();
    }

    public static async Task<IEnumerable<string>> ReadLinesAsync(string filePath, Encoding encoding)
    {
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));

        using var stream = FileStreamEx.OpenAsyncReadFileStream(filePath);
        using var reader = new StaticEncodingLineReader(stream, encoding);
        return await reader.ReadLinesAsync().ConfigureAwait(false);
    }

    public string? ReadLine() => _reader.ReadLine();

    public Task<string?> ReadLineAsync() => _reader.ReadLineAsync();

    public void Dispose()
    {
        _reader.Dispose();
        _stream.Dispose();
    }
}
