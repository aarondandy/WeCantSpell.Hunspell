using System;

namespace Hunspell
{
#if PRE_CORE
    [Serializable]
#endif
    [Obsolete("This exception appears to be unused and may be removed")]
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
