using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class StringBuilderPool
    {
        private const int MaxCachedBuilderCapacity = WordList.MaxWordLen;

        [ThreadStatic]
        private static StringBuilder PrimaryCache;

        [ThreadStatic]
        private static StringBuilder SecondaryCache;

        [ThreadStatic]
        private static StringBuilder TertiaryCache;

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get() => GetClearedBuilder();

        public static StringBuilder Get(string value) =>
            GetClearedBuilderWithCapacity(value.Length).Append(value);

        public static StringBuilder Get(string value, int capacity) =>
            GetClearedBuilderWithCapacity(capacity).Append(value);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get(int capacity) =>
            GetClearedBuilderWithCapacity(capacity);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get(string value, int valueStartIndex, int valueLength) =>
            Get(value, valueStartIndex, valueLength, valueLength);

        public static StringBuilder Get(string value, int valueStartIndex, int valueLength, int capacity) =>
            GetClearedBuilderWithCapacity(capacity).Append(value, valueStartIndex, valueLength);

        internal static StringBuilder Get(StringSlice value) =>
            Get(value.Text, value.Offset, value.Length, value.Length);

#if !NO_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Return(StringBuilder builder)
        {
            if (builder != null && builder.Capacity <= MaxCachedBuilderCapacity)
            {
                if (PrimaryCache == null)
                {
                    PrimaryCache = builder;
                }
                else if (SecondaryCache == null)
                {
                    SecondaryCache = builder;
                }
                else
                {
                    TertiaryCache = builder;
                }
            }
        }

#if !NO_INLINE
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
            var result = ReferenceHelpers.Steal(ref PrimaryCache);
            if (result == null)
            {
                result = ReferenceHelpers.Steal(ref SecondaryCache);
                if (result == null)
                {
                    result = ReferenceHelpers.Steal(ref TertiaryCache);
                    if (result == null)
                    {
                        return new StringBuilder();
                    }
                }
            }

            return result.Clear();
        }

        private static StringBuilder GetClearedBuilderWithCapacity(int capacity)
        {
            if (capacity > MaxCachedBuilderCapacity)
            {
                return new StringBuilder(capacity);
            }

            var result = StealForCapacity(ref PrimaryCache, capacity);
            if (result == null)
            {
                result = StealForCapacity(ref SecondaryCache, capacity);
                if (result == null)
                {
                    result = StealForCapacity(ref TertiaryCache, capacity);
                    if (result == null)
                    {
                        return new StringBuilder(capacity);
                    }
                }
            }

            return result.Clear();
        }

        private static StringBuilder StealForCapacity(ref StringBuilder source, int minimumCapacity)
        {
            var taken = source;
            if (taken == null || source.Capacity < minimumCapacity)
            {
                return null;
            }

            source = null;
            return taken;
        }
    }
}
