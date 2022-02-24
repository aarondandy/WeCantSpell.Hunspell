using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public class MultiReplacementTable : IReadOnlyDictionary<string, MultiReplacementEntry>
{
    public static readonly MultiReplacementTable Empty = TakeDictionary(new Dictionary<string, MultiReplacementEntry>(0));

    public static MultiReplacementTable Create(IEnumerable<KeyValuePair<string, MultiReplacementEntry>>? replacements) =>
        replacements is null ? Empty : TakeDictionary(replacements.ToDictionary(s => s.Key, s => s.Value));

    internal static MultiReplacementTable TakeDictionary(Dictionary<string, MultiReplacementEntry>? replacements) =>
        replacements is null ? Empty : new MultiReplacementTable(replacements);

    private MultiReplacementTable(Dictionary<string, MultiReplacementEntry> replacements)
    {
        _replacements = replacements;
    }

    private readonly Dictionary<string, MultiReplacementEntry> _replacements;

    public MultiReplacementEntry this[string key] => _replacements[key];

    public int Count => _replacements.Count;

    public bool HasReplacements => _replacements.Count > 0;

    public IEnumerable<string> Keys => _replacements.Keys;

    public IEnumerable<MultiReplacementEntry> Values => _replacements.Values;

    public bool ContainsKey(string key) => _replacements.ContainsKey(key);

    public bool TryGetValue(string key, out MultiReplacementEntry value) => _replacements.TryGetValue(key, out value);

    internal bool TryConvert(string text, out string converted)
    {
#if DEBUG
        if (text is null) throw new ArgumentNullException(nameof(text));
#endif

        var appliedConversion = false;

        if (text.Length == 0)
        {
            converted = text;
        }
        else
        {
            var convertedBuilder = StringBuilderPool.Get(text.Length);

            for (var i = 0; i < text.Length; i++)
            {
                var replacementEntry = FindLargestMatchingConversion(text.AsSpan(i));
                if (replacementEntry != null)
                {
                    var replacementText = replacementEntry.ExtractReplacementText(text.Length - i, i == 0);
                    if (!string.IsNullOrEmpty(replacementText))
                    {
                        convertedBuilder.Append(replacementText);
                        i += replacementEntry.Pattern.Length - 1;
                        appliedConversion = true;
                        continue;
                    }
                }

                convertedBuilder.Append(text[i]);
            }

            converted = StringBuilderPool.GetStringAndReturn(convertedBuilder);
        }

        return appliedConversion;
    }

    /// <summary>
    /// Finds a conversion matching the longest version of the given <paramref name="text"/> from the left.
    /// </summary>
    /// <param name="text">The text to find a matching input conversion for.</param>
    /// <returns>The best matching input conversion.</returns>
    /// <seealso cref="MultiReplacementEntry"/>
    internal MultiReplacementEntry FindLargestMatchingConversion(ReadOnlySpan<char> text)
    {
        for (var searchLength = text.Length; searchLength > 0; searchLength--)
        {
            if (_replacements.TryGetValue(text.Slice(0, searchLength).ToString(), out var entry))
            {
                return entry;
            }
        }

        return null;
    }

    internal Dictionary<string, MultiReplacementEntry>.Enumerator GetEnumerator() => _replacements.GetEnumerator();

    IEnumerator<KeyValuePair<string, MultiReplacementEntry>> IEnumerable<KeyValuePair<string, MultiReplacementEntry>>.GetEnumerator() => _replacements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _replacements.GetEnumerator();
}
