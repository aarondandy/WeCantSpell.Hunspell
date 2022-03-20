﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public class MultiReplacementTable : IReadOnlyDictionary<string, MultiReplacementEntry>
{
    public static readonly MultiReplacementTable Empty = TakeDictionary(new TextDictionary<MultiReplacementEntry>(0));

    public static MultiReplacementTable Create(Dictionary<string, MultiReplacementEntry>? replacements)
    {
        if (replacements is null)
        {
            return Empty;
        }

        var result = new TextDictionary<MultiReplacementEntry>(replacements.Count);
        foreach (var replacement in replacements)
        {
            result.Add(replacement.Key, replacement.Value);
        }

        return TakeDictionary(result);
    }

    internal static MultiReplacementTable TakeDictionary(TextDictionary<MultiReplacementEntry>? replacements) =>
        replacements is null ? Empty : new MultiReplacementTable(replacements);

    private MultiReplacementTable(TextDictionary<MultiReplacementEntry> replacements)
    {
        _replacements = replacements;
    }

    private readonly TextDictionary<MultiReplacementEntry> _replacements;

    public MultiReplacementEntry this[string key]
    {
        get
        {
            if (_replacements.TryGetValue(key, out var result))
            {
                return result;
            }

            throw new InvalidOperationException();
        }
    }

    public int Count => _replacements.Count;

    public bool HasReplacements => _replacements.Count > 0;

    public IEnumerable<string> Keys => _replacements.Keys.Select(key => key.ToString());

    public IEnumerable<MultiReplacementEntry> Values => _replacements.Select(entry => entry.Value);

    public bool ContainsKey(string key) => _replacements.ContainsKey(key);

    public bool TryGetValue(
        string key,
#if !NO_EXPOSED_NULLANNOTATIONS
        [MaybeNullWhen(false)]
#endif
        out MultiReplacementEntry value
    ) => _replacements.TryGetValue(key, out value);

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
                if (
                    FindLargestMatchingConversion(text.AsSpan(i)) is { } replacementEntry
                    && replacementEntry.ExtractReplacementText(text.Length - i, i == 0) is { Length: > 0 } replacementText)
                {
                    convertedBuilder.Append(replacementText);
                    i += replacementEntry.Pattern.Length - 1;
                    appliedConversion = true;
                }
                else
                {
                    convertedBuilder.Append(text[i]);
                }
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
    internal MultiReplacementEntry? FindLargestMatchingConversion(ReadOnlySpan<char> text)
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

    public IEnumerator<KeyValuePair<string, MultiReplacementEntry>> GetEnumerator() => _replacements
        .Select(entry => new KeyValuePair<string, MultiReplacementEntry>(entry.Key.ToString(), entry.Value))
        .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _replacements.GetEnumerator();
}
