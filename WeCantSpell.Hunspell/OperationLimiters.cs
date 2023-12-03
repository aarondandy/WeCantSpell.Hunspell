using System;
using System.Threading;

namespace WeCantSpell.Hunspell;

struct OperationTimedLimiter
{
    public OperationTimedLimiter(TimeSpan timeLimit, CancellationToken cancellationToken)
    {
        _timer = new ExpirationTimer(timeLimit);
        _cancellationToken = cancellationToken;
        _hasTriggeredCancellation = false;
    }

    private readonly ExpirationTimer _timer;
    private readonly CancellationToken _cancellationToken;
    private bool _hasTriggeredCancellation;

    public readonly bool HasBeenCanceled => _hasTriggeredCancellation || _cancellationToken.IsCancellationRequested;

    public bool QueryForCancellation()
    {
        if (!_hasTriggeredCancellation)
        {
            if (_cancellationToken.IsCancellationRequested || _timer.CheckForExpiration())
            {
                _hasTriggeredCancellation = true;
            }
        }

        return _hasTriggeredCancellation;
    }
}

struct OperationTimedCountLimiter
{
    /// <summary>
    /// This is the number of operations that are added to a limiter if it runs out of operations before the time limit has expired.
    /// </summary>
    private const int MaxPlusTimer = 100;

    public OperationTimedCountLimiter(TimeSpan timeLimit, int countLimit, CancellationToken cancellationToken)
    {
        _timer = new ExpirationTimer(timeLimit);
        _cancellationToken = cancellationToken;
        _counter = countLimit;
        _hasTriggeredCancellation = false;
    }

    private readonly ExpirationTimer _timer;
    private readonly CancellationToken _cancellationToken;
    private int _counter;
    private bool _hasTriggeredCancellation;

    public readonly bool HasBeenCanceled => _hasTriggeredCancellation || _cancellationToken.IsCancellationRequested;

    public bool QueryForCancellation()
    {
        if (!_hasTriggeredCancellation)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                _hasTriggeredCancellation = true;
            }
            else if (_counter > 1)
            {
                _counter--;
            }
            else if (_timer.CheckForExpiration())
            {
                _counter = MaxPlusTimer;
            }
            else
            {
                _counter = 0;
                _hasTriggeredCancellation = true;
            }
        }

        return _hasTriggeredCancellation;
    }
}

readonly struct ExpirationTimer
{
    private const long DisabledSentinelValue = long.MinValue;

    private static long GetCurrentTicks() => DateTime.UtcNow.Ticks;

    internal ExpirationTimer(TimeSpan timeLimit)
    {
        var limitTicks = timeLimit.Ticks;
        if (limitTicks < 0)
        {
            _expiresAt = DisabledSentinelValue;
        }
        else
        {
            _expiresAt = GetCurrentTicks() + limitTicks;
            if (_expiresAt < DateTime.MinValue.Ticks || _expiresAt > DateTime.MaxValue.Ticks)
            {
                _expiresAt = DisabledSentinelValue;
            }
        }
    }

    private readonly long _expiresAt;

    public readonly bool CheckForExpiration() => _expiresAt != DisabledSentinelValue && _expiresAt <= GetCurrentTicks();
}
