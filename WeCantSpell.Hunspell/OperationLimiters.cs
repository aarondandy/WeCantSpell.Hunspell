using System;

namespace WeCantSpell.Hunspell;

sealed class OperationTimedLimiter
{
    public OperationTimedLimiter(TimeSpan timeLimit)
        : this((int)timeLimit.TotalMilliseconds)
    {
    }

    public OperationTimedLimiter(int timeLimitMs)
    {
        _startedAtMs = Environment.TickCount;
        _timeLimitMs = timeLimitMs;
        _expiresAtMs = _startedAtMs + timeLimitMs;
    }

    private bool _hasTriggeredCancellation;
    private int _expiresAtMs;
    private int _startedAtMs;
    private readonly int _timeLimitMs;

    public bool QueryForCancellation()
    {
        if (!_hasTriggeredCancellation)
        {
            if (_expiresAtMs <= Environment.TickCount)
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
    public OperationTimedCountLimiter(TimeSpan timeLimit, int countLimit)
        : this((int)timeLimit.TotalMilliseconds, countLimit)
    {
    }

    public OperationTimedCountLimiter(int timeLimitMs, int countLimit)
    {
#if DEBUG
        if (countLimit < 0) throw new ArgumentOutOfRangeException(nameof(countLimit));
#endif

        _startedAtMs = Environment.TickCount;
        _timeLimitMs = timeLimitMs;
        _expiresAtMs = _startedAtMs + timeLimitMs;
        _countLimit = countLimit;
        _counter = countLimit;
    }

    private bool _hasTriggeredCancellation;
    private int _counter;
    private int _expiresAtMs;
    private int _startedAtMs;
    private readonly int _timeLimitMs;
    private readonly int _countLimit;

    public bool QueryForCancellation()
    {
        if (!_hasTriggeredCancellation)
        {
            if (_counter > 0)
            {
                _counter--;
                return false;
            }

            if (_expiresAtMs <= Environment.TickCount)
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
