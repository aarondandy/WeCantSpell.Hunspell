using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeCantSpell.Hunspell;

/// <summary>
/// Defines operations to read affix or dictionary lines from a stream sequentially.
/// </summary>
[Obsolete("Direct usage of specific implementations is probably better")]
public interface IHunspellLineReader
{
    /// <summary>
    /// Reads the next line from a stream.
    /// </summary>
    /// <returns>A task that represents the asynchronous read operation. The reult value will contain the contents of the next line as a string or the value <c>null</c> indicating there are no more lines to be read.</returns>
    Task<string?> ReadLineAsync();

    /// <summary>
    /// Reads the next line from a stream.
    /// </summary>
    /// <returns></returns>
    string? ReadLine();

    /// <summary>
    /// Gets the current encoding that the reader is using to decode text.
    /// </summary>
    Encoding CurrentEncoding { get; }
}

public static class HunspellLineReaderExtensions
{
    public static async Task<IEnumerable<string>> ReadLinesAsync(this IHunspellLineReader reader)
    {
        if (reader is null) throw new ArgumentNullException(nameof(reader));

        var lines = new List<string>();

        string? line;
        while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) is not null)
        {
            lines.Add(line);
        }

        return lines;
    }

    public static IEnumerable<string> ReadLines(this IHunspellLineReader reader)
    {
        if (reader is null) throw new ArgumentNullException(nameof(reader));

        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            yield return line;
        }
    }
}
