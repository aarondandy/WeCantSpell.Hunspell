using System;
using System.Threading;

namespace WeCantSpell.Hunspell;

sealed class OperationTimedLimiter
{
    public OperationTimedLimiter(TimeSpan timeLimit, CancellationToken cancellationToken)
        : this((int)timeLimit.TotalMilliseconds, cancellationToken)
    {
    }

    public OperationTimedLimiter(int timeLimitMs, CancellationToken cancellationToken)
    {
        _startedAtMs = Environment.TickCount;
        _timeLimitMs = timeLimitMs;
        _expiresAtMs = _startedAtMs + timeLimitMs;
        _cancellationToken = cancellationToken;
    }

    private bool _hasTriggeredCancellation;
    private int _expiresAtMs;
    private int _startedAtMs;
    private readonly CancellationToken _cancellationToken;
    private readonly int _timeLimitMs;

    public bool QueryForCancellation()
    {
        if (!_hasTriggeredCancellation)
        {
            if (_cancellationToken.IsCancellationRequested || _expiresAtMs <= Environment.TickCount)
            {
                _hasTriggeredCancellation = true;
            }
        }

        return _hasTriggeredCancellation;
    }

    public void Reset()
    {
        _hasTriggeredCancellation = false;
        _startedAtMs = Environment.TickCount;
        _expiresAtMs = _startedAtMs + _timeLimitMs;
    }
}

sealed class OperationTimedCountLimiter
{
    public OperationTimedCountLimiter(TimeSpan timeLimit, int countLimit, CancellationToken cancellationToken)
        : this((int)timeLimit.TotalMilliseconds, countLimit, cancellationToken)
    {
    }

    public OperationTimedCountLimiter(int timeLimitMs, int countLimit, CancellationToken cancellationToken)
    {
#if DEBUG
        if (countLimit < 0) throw new ArgumentOutOfRangeException(nameof(countLimit));
#endif

        _startedAtMs = Environment.TickCount;
        _timeLimitMs = timeLimitMs;
        _expiresAtMs = _startedAtMs + timeLimitMs;
        _countLimit = countLimit;
        _counter = countLimit;
        _cancellationToken = cancellationToken;
    }

    private bool _hasTriggeredCancellation;
    private int _counter;
    private int _expiresAtMs;
    private int _startedAtMs;
    private readonly CancellationToken _cancellationToken;
    private readonly int _timeLimitMs;
    private readonly int _countLimit;

    public bool HasTriggeredCancellation => _hasTriggeredCancellation;

    public bool QueryForCancellation()
    {
        if (!_hasTriggeredCancellation)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                _hasTriggeredCancellation = true;
            }
            else if (_counter > 0)
            {
                _counter--;
                return false;
            }
            else if (_expiresAtMs <= Environment.TickCount)
            {
                _hasTriggeredCancellation = true;
            }
        }

        return _hasTriggeredCancellation;
    }

    public void Reset()
    {
        _counter = _countLimit;
        _hasTriggeredCancellation = false;
        _startedAtMs = Environment.TickCount;
        _expiresAtMs = _startedAtMs + _timeLimitMs;
    }
}
