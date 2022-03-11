using System;
using System.Threading;

namespace WeCantSpell.Hunspell;

class OperationLimiter : IDisposable
{
    public OperationLimiter(TimeSpan timeLimit, int countLimit)
    {
#if DEBUG
        if (countLimit < 0) throw new ArgumentOutOfRangeException(nameof(countLimit));
#endif

        _cts = new CancellationTokenSource(timeLimit);
        Token = _cts.Token;
        CountLimit = countLimit;
        Counter = CountLimit;
    }

    private readonly CancellationTokenSource? _cts;

    public CancellationToken Token { get; }
    public int CountLimit { get; }
    public int Counter { get; private set; }

    public bool QueryForCountCancellation()
    {
        if (Counter > 0)
        {
            Counter--;
            return false;
        }
        else
        {
            return Token.IsCancellationRequested;
        }
    }

    public void Dispose()
    {
        _cts?.Dispose();
    }
}
