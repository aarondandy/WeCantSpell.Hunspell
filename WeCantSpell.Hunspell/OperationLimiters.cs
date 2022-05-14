﻿using System;
using System.Threading;

namespace WeCantSpell.Hunspell;

struct OperationTimedLimiter
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
        _hasTriggeredCancellation = false;
    }

    private int _expiresAtMs;
    private int _startedAtMs;
    private readonly int _timeLimitMs;
    private readonly CancellationToken _cancellationToken;
    private bool _hasTriggeredCancellation;

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
    /// <summary>
    /// This is the number of operations that are added to a timer if it runs out of operations
    /// before the time limit has expired.
    /// </summary>
    private const int MaxPlusTimer = 100;

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
        _hasTriggeredCancellation = false;
    }

    private readonly int _timeLimitMs;
    private readonly int _countLimit;
    private int _counter;
    private int _expiresAtMs;
    private int _startedAtMs;
    private readonly CancellationToken _cancellationToken;
    private bool _hasTriggeredCancellation;

    public bool HasBeenCanceled => _hasTriggeredCancellation || _cancellationToken.IsCancellationRequested;

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
            else if (_expiresAtMs > Environment.TickCount)
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

    public void Reset()
    {
        _counter = _countLimit;
        _hasTriggeredCancellation = false;
        _startedAtMs = Environment.TickCount;
        _expiresAtMs = _startedAtMs + _timeLimitMs;
    }
}
