using System.Collections.Generic;

namespace WeCantSpell.Hunspell;

internal sealed class CandidateStack : List<string>
{
    public CandidateStack() : base(1)
    {
        // Preallocate with a small capacity as it doesn't often grow very large
    }

    public void Push(string value)
    {
        Add(value);
    }

    public void Pop()
    {
        if (Count > 0)
        {
            RemoveAt(Count - 1);
        }
    }
}
