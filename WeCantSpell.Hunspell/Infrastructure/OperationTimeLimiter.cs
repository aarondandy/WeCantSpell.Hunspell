using System;

namespace WeCantSpell.Hunspell.Infrastructure;

class OperationTimeLimiter
{
    public static OperationTimeLimiter Create(int timeLimitInMs, int queriesToTriggerCheck) =>
        new OperationTimeLimiter(
            Environment.TickCount,
            queriesToTriggerCheck,
            timeLimitInMs);

    public static OperationTimeLimiter Create(int timeLimitInMs) =>
        Create(timeLimitInMs, 0);

    private OperationTimeLimiter(
        long operationStartTime,
        int queriesToTriggerCheck,
        int timeLimitInMs)
    {
#if DEBUG
        if (queriesToTriggerCheck < 0) throw new ArgumentOutOfRangeException(nameof(queriesToTriggerCheck));
#endif

        _operationStartTime = operationStartTime;
        _queriesToTriggerCheck = queriesToTriggerCheck;
        _timeLimitInMs = timeLimitInMs;
        QueryCounter = queriesToTriggerCheck;
        HasExpired = false;
    }

    private long _operationStartTime;
    private readonly int _queriesToTriggerCheck;
    private readonly int _timeLimitInMs;

    public int QueryCounter { get; private set; }

    public bool HasExpired { get; private set; }

    public bool QueryForExpiration()
    {
        if (!HasExpired)
        {
            if (QueryCounter == 0)
            {
                HandleQueryCounterTrigger();
            }
            else
            {
                QueryCounter--;
            }
        }

        return HasExpired;
    }

    public void Reset()
    {
        _operationStartTime = Environment.TickCount;
        QueryCounter = _queriesToTriggerCheck;
        HasExpired = false;
    }

    private void HandleQueryCounterTrigger()
    {
        var currentTicks = Environment.TickCount - _operationStartTime;
        if (currentTicks > _timeLimitInMs)
        {
            HasExpired = true;
        }

        QueryCounter = _queriesToTriggerCheck;
    }
}
