using System;
using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell.Infrastructure;

sealed class IncrementalWordList
{
    public IncrementalWordList() : this(new(), 0)
    {
    }

    public IncrementalWordList(List<WordEntryDetail?> words, int wNum)
    {
#if DEBUG
        if (wNum < 0) throw new ArgumentOutOfRangeException(nameof(wNum));
#endif
        Words = words;
        WNum = wNum;
    }

    internal readonly List<WordEntryDetail?> Words;
    internal readonly int WNum;

    public void SetCurrent(WordEntryDetail value)
    {
        if (WNum == Words.Count)
        {
            Words.Add(value);
        }
        else if (WNum < Words.Count)
        {
            Words[WNum] = value;
        }
        else
        {
            Words.AddRange(Enumerable.Repeat<WordEntryDetail?>(null, Math.Max(WNum - Words.Count, 0)).Append(value));
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
#if DEBUG
        if (wordIndex < 0) throw new ArgumentOutOfRangeException(nameof(wordIndex));
#endif

        return wordIndex < Words.Count
            && Words[wordIndex] is { } detail
            && detail.ContainsFlag(flag);
    }

    public IncrementalWordList CreateIncremented() => new(Words, WNum + 1);
}
