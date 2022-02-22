using System;
using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

class IncrementalWordList
{
    public IncrementalWordList()
        : this(new List<WordEntryDetail>(), 0) { }

    public IncrementalWordList(List<WordEntryDetail> words, int wNum)
    {
#if DEBUG
        if (words is null) throw new ArgumentNullException(nameof(words));
        if (WNum < 0) throw new ArgumentOutOfRangeException(nameof(wNum));
#endif
        Words = words;
        WNum = wNum;
    }

    public List<WordEntryDetail> Words { get; }

    public int WNum { get; }

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
            appendWithLeadingBlanks();
            void appendWithLeadingBlanks()
            {
                for (var i = WNum - Words.Count; i > 0; i--)
                {
                    Words.Add(null);
                }

                Words.Add(value);
            }
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

    private bool CheckIfNotNull(int index) => (index < Words.Count) && Words[index] is not null;

    public bool ContainsFlagAt(int wordIndex, FlagValue flag)
    {
#if DEBUG
        if (wordIndex < 0) throw new ArgumentOutOfRangeException(nameof(wordIndex));
#endif

        if (wordIndex < Words.Count)
        {
            var detail = Words[wordIndex];
            if (detail != null)
            {
                return detail.ContainsFlag(flag);
            }
        }

        return false;
    }

    public IncrementalWordList CreateIncremented() => new(Words, WNum + 1);
}
