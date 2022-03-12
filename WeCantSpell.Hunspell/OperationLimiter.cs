using System;
using System.Threading;

namespace WeCantSpell.Hunspell;

sealed class OperationLimiter : IDisposable
{
    public OperationLimiter(TimeSpan timeLimit, int countLimit)
    {
#if DEBUG
        if (countLimit < 0) throw new ArgumentOutOfRangeException(nameof(countLimit));
#endif

        _cts = new CancellationTokenSource(timeLimit);
        _counter = countLimit;
    }

    private readonly CancellationTokenSource _cts;
    private int _counter;

    public bool QueryForCountCancellation()
    {
        if (_counter > 0)
        {
            _counter--;
            return false;
        }
        else
        {
            return _cts.IsCancellationRequested;
        }
    }

    public void Dispose()
    {
        _cts.Dispose();
    }
}
