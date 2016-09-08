using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hunspell.Infrastructure
{
    internal static class StringBuilderPool
    {
        private const int MaxCachedBuilderCapacity = 360; // same value used for the framework

        [ThreadStatic]
        private static StringBuilder ThreadCache;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get()
        {
            return GetClearedBuilder();
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get(string value)
        {
            return GetClearedBuilder()
                .Append(value);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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
            if (builder.Capacity <= MaxCachedBuilderCapacity)
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
            var result = ThreadCache;
            if (result == null || capacity > MaxCachedBuilderCapacity)
            {
                result = new StringBuilder(capacity);
            }
            else
            {
                ThreadCache = null;
                if (result.Capacity < capacity)
                {
                    result = new StringBuilder(capacity);
                }
                else
                {
                    result.Clear();
                }
            }

            return result;
        }
    }
}
