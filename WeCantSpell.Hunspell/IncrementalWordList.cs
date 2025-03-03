using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("WNum = {WNum}")]
internal sealed class IncrementalWordList
{
    private const int MaxCachedCapacity = 32;

    private static IncrementalWordList? PoolCache;

    public static IncrementalWordList GetRoot()
    {
        if (Interlocked.Exchange(ref PoolCache, null) is { } rental)
        {
#if DEBUG
            if (rental.WNum != 0) ExceptionEx.ThrowInvalidOperation();
#endif
            rental._words.Clear();
        }
        else
        {
            rental = new();
        }

        return rental;
    }

    public static void ReturnRoot(ref IncrementalWordList? rental)
    {
        if (rental is { _words.Capacity: > 0 and <= MaxCachedCapacity })
        {
#if DEBUG
            if (rental.WNum != 0) ExceptionEx.ThrowInvalidOperation();
#endif

            Volatile.Write(ref PoolCache, rental);
        }

        rental = null;
    }

    private IncrementalWordList() : this([], 0)
    {
    }

    private IncrementalWordList(List<WordEntryDetail?> words, int wNum)
    {
        _words = words;
        WNum = wNum;
    }

    private readonly List<WordEntryDetail?> _words;

    public readonly int WNum;

    public void SetCurrent(in WordEntryDetail value)
    {
        if (WNum < _words.Count)
        {
            _words[WNum] = value;
        }
        else
        {
            while (WNum > _words.Count)
            {
                _words.Add(null);
            }

            _words.Add(value);
        }
    }

    public void ClearCurrent()
    {
        if (WNum < _words.Count)
        {
            _words[WNum] = null;
        }
    }

    public bool CheckIfCurrentIsNotNull() => CheckIfNotNull(WNum);

    public bool CheckIfNextIsNotNull() => CheckIfNotNull(WNum + 1);

    private bool CheckIfNotNull(int index) => index < _words.Count && _words[index] is not null;

    public bool ContainsFlagAt(int index, FlagValue flag)
    {
        return index < _words.Count
            && _words[index] is { } word
            && word.ContainsFlag(flag);
    }

    public IncrementalWordList CreateIncremented() => new(_words, WNum + 1);
}
