using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class IncrementalWordList
{
    public IncrementalWordList() : this([], 0)
    {
    }

    public IncrementalWordList(List<WordEntryDetail?> words, int wNum)
    {
#if DEBUG && HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(wNum, 0);
#endif
        Words = words;
        WNum = wNum;
    }

    internal readonly List<WordEntryDetail?> Words;
    internal readonly int WNum;

    public void SetCurrent(WordEntryDetail value)
    {
        if (WNum < Words.Count)
        {
            Words[WNum] = value;
        }
        else
        {
            while (WNum > Words.Count)
            {
                Words.Add(null);
            }

            Words.Add(value);
        }
    }

    public void ClearCurrent()
    {
        if (WNum < Words.Count)
        {
            Words[WNum] = null;
        }
    }

    public bool CheckIfCurrentIsNotNull() => CheckIfNotNull(WNum);

    public bool CheckIfNextIsNotNull() => CheckIfNotNull(WNum + 1);

    private bool CheckIfNotNull(int index) => index < Words.Count && Words[index] is not null;

    public bool ContainsFlagAt(int wordIndex, FlagValue flag)
    {
#if DEBUG && HAS_THROWOOR
        ArgumentOutOfRangeException.ThrowIfLessThan(wordIndex, 0);
#endif

        return wordIndex < Words.Count
            && Words[wordIndex] is { } detail
            && detail.ContainsFlag(flag);
    }

    public IncrementalWordList CreateIncremented() => new(Words, WNum + 1);
}
