using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hunspell.Infrastructure
{
    internal static class StringBuilderPool
    {
        private const int MaxCachedBuilderCapacity = HunspellQueryState.MaxWordLen;

        [ThreadStatic]
        private static StringBuilder ThreadCache;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get()
        {
            return GetClearedBuilder();
        }

        public static StringBuilder Get(string value)
        {
            return GetClearedBuilderWithCapacity(value.Length)
                .Append(value);
        }

        public static StringBuilder Get(string value, int capacity)
        {
            return GetClearedBuilderWithCapacity(capacity)
                .Append(value);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get(int capacity)
        {
            return GetClearedBuilderWithCapacity(capacity);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get(string value, int valueStartIndex, int valueLength)
        {
            return Get(value, valueStartIndex, valueLength, valueLength);
        }

        public static StringBuilder Get(string value, int valueStartIndex, int valueLength, int capacity)
        {
            return GetClearedBuilderWithCapacity(capacity)
                .Append(value, valueStartIndex, valueLength);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Return(StringBuilder builder)
        {
            if (builder != null && builder.Capacity <= MaxCachedBuilderCapacity)
            {
                ThreadCache = builder;
            }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static string GetStringAndReturn(StringBuilder builder)
        {
            var value = builder.ToString();
            Return(builder);
            return value;
        }

        private static StringBuilder GetClearedBuilder()
        {
            var result = ThreadCache;
            if (result == null)
            {
                result = new StringBuilder();
            }
            else
            {
                ThreadCache = null;
                result.Clear();
            }

            return result;
        }

        private static StringBuilder GetClearedBuilderWithCapacity(int capacity)
        {
            if (capacity <= MaxCachedBuilderCapacity)
            {
                var result = ThreadCache;
                if (result != null)
                {
                    ThreadCache = null;
                    if (result.Capacity >= capacity)
                    {
                        result.Clear();
                        return result;
                    }

                }
            }

            return new StringBuilder(capacity);
        }
    }
}
