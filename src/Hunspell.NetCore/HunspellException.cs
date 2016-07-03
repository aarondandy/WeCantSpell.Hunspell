using System;

namespace Hunspell
{
#if PRE_CORE
    [Serializable]
#endif
    public sealed class HunspellException : Exception
    {
        public HunspellException(string message) : base(message)
        {
        }

#if PRE_CORE
        private HunspellException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
