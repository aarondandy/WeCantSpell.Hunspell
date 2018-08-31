using System;

namespace WeCantSpell.Hunspell.Infrastructure
{
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

            OperationStartTime = operationStartTime;
            QueriesToTriggerCheck = queriesToTriggerCheck;
            TimeLimitInMs = timeLimitInMs;
            QueryCounter = queriesToTriggerCheck;
            HasExpired = false;
        }

        private long OperationStartTime;

        private int QueriesToTriggerCheck;

        private int TimeLimitInMs;

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
            OperationStartTime = Environment.TickCount;
            QueryCounter = QueriesToTriggerCheck;
            HasExpired = false;
        }

        private void HandleQueryCounterTrigger()
        {
            var currentTicks = Environment.TickCount - OperationStartTime;
            if (currentTicks > TimeLimitInMs)
            {
                HasExpired = true;
            }

            QueryCounter = QueriesToTriggerCheck;
        }
    }
}
