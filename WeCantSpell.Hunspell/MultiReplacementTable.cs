using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using WeCantSpell.Hunspell.Infrastructure;

namespace WeCantSpell.Hunspell;

public sealed class MultiReplacementTable : IReadOnlyDictionary<string, MultiReplacementEntry>
{
    public static readonly MultiReplacementTable Empty = TakeDictionary(new TextDictionary<MultiReplacementEntry>(0));

    public static MultiReplacementTable Create(Dictionary<string, MultiReplacementEntry>? replacements) =>
        replacements is null
            ? Empty
            : TakeDictionary(TextDictionary<MultiReplacementEntry>.MapFromDictionary(replacements));

    internal static MultiReplacementTable Create(TextDictionary<MultiReplacementEntry>? replacements) =>
        replacements is null
            ? Empty
            : TakeDictionary(TextDictionary<MultiReplacementEntry>.Clone(replacements));

    internal static MultiReplacementTable TakeDictionary(TextDictionary<MultiReplacementEntry>? replacements) =>
        replacements is null ? Empty : new MultiReplacementTable(replacements);

    private MultiReplacementTable(TextDictionary<MultiReplacementEntry> replacements)
    {
        _replacements = replacements;
    }

    private readonly TextDictionary<MultiReplacementEntry> _replacements;

    public MultiReplacementEntry this[string key] => _replacements[key];

    public int Count => _replacements.Count;

    public bool HasReplacements => _replacements.Count > 0;

    public IEnumerable<string> Keys => _replacements.Keys;

    public IEnumerable<MultiReplacementEntry> Values => _replacements.Values;

    public IEnumerator<KeyValuePair<string, MultiReplacementEntry>> GetEnumerator() => _replacements.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
        if (text.Length != 0)
        {
            var appliedConversion = false;
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

            if (appliedConversion)
            {
                converted = StringBuilderPool.GetStringAndReturn(convertedBuilder);
                return true;
            }
            else
            {
                StringBuilderPool.Return(convertedBuilder);
            }
        }

        converted = string.Empty;
        return false;
    }

    internal bool TryConvert(ReadOnlySpan<char> text, out string converted)
    {
        if (!text.IsEmpty)
        {
            var appliedConversion = false;
            var convertedBuilder = StringBuilderPool.Get(text.Length);

            for (var i = 0; i < text.Length; i++)
            {
                if (
                    FindLargestMatchingConversion(text.Slice(i)) is { } replacementEntry
                    && replacementEntry.ExtractReplacementText(text.Length - i, i == 0) is { Length: > 0 } replacementText)
                {
                    convertedBuilder.Append(replacementText);

                    if (replacementEntry.Pattern.Length > 1)
                    {
                        i += replacementEntry.Pattern.Length - 1;
                    }

                    appliedConversion = true;
                }
                else
                {
                    convertedBuilder.Append(text[i]);
                }
            }

            if (appliedConversion)
            {
                converted = StringBuilderPool.GetStringAndReturn(convertedBuilder);
                return true;
            }
            else
            {
                StringBuilderPool.Return(convertedBuilder);
            }
        }

        converted = string.Empty;
        return false;
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
            if (_replacements.TryGetValue(text.Slice(0, searchLength), out var entry))
            {
                return entry;
            }
        }

        return null;
    }
}
