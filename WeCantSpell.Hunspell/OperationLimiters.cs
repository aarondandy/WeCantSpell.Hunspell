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
            if (_cancellationToken.IsCancellationRequested || _timer.QueryForExpiration())
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
    /// <remarks>
    /// The prupose of this mechanism seems to be a reduction in the number of slow queries made against the clock.
    /// </remarks>
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
            else if (_timer.QueryForExpiration())
            {
                _counter = 0;
                _hasTriggeredCancellation = true;
            }
            else
            {
                _counter = MaxPlusTimer;
            }
        }

        return _hasTriggeredCancellation;
    }
}

readonly struct ExpirationTimer
{
    private static readonly DateTime DisabledSentinelValue = DateTime.MinValue;

    internal ExpirationTimer(TimeSpan timeLimit)
    {
        if (timeLimit < TimeSpan.Zero)
        {
            _expiresAt = DisabledSentinelValue;
        }
        else
        {
            _expiresAt = DateTime.UtcNow + timeLimit;
            if (DateTime.MinValue >= _expiresAt || DateTime.MaxValue <= _expiresAt)
            {
                _expiresAt = DisabledSentinelValue;
            }
        }
    }

    private readonly DateTime _expiresAt;

    public readonly bool QueryForExpiration() => _expiresAt != DisabledSentinelValue && DateTime.UtcNow >= _expiresAt;
}
