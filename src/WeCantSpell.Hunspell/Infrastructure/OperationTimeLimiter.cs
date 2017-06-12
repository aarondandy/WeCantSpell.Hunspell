using System;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal class OperationTimeLimiter
    {
        public static OperationTimeLimiter Create(int timeLimitInMs, int queriesToTriggerCheck) =>
            new OperationTimeLimiter(
                Environment.TickCount,
                queriesToTriggerCheck,
                timeLimitInMs);

        private readonly long operationStartTime;
        private readonly int queriesToTriggerCheck;
        private readonly int timeLimitInMs;
        private int queryCounter;
        private bool expirationTriggered;

        private OperationTimeLimiter(
            long operationStartTime,
            int queriesToTriggerCheck,
            int timeLimitInMs)
        {
            if (queriesToTriggerCheck < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(queriesToTriggerCheck));
            }

            this.operationStartTime = operationStartTime;
            this.queriesToTriggerCheck = queriesToTriggerCheck;
            this.timeLimitInMs = timeLimitInMs;
            queryCounter = queriesToTriggerCheck;
            expirationTriggered = false;
        }

        public int QueryCounter => queryCounter;

        public bool HasExpired => expirationTriggered;

        public bool QueryForExpiration()
        {
            if (!expirationTriggered)
            {
                if (queryCounter == 0)
                {
                    HandleQueryCounterTrigger();
                }
                else
                {
                    queryCounter--;
                }
            }

            return expirationTriggered;
        }

        private void HandleQueryCounterTrigger()
        {
            var currentTicks = Environment.TickCount - operationStartTime;
            if (currentTicks > timeLimitInMs)
            {
                expirationTriggered = true;
            }

            queryCounter = queriesToTriggerCheck;
        }
    }
}
