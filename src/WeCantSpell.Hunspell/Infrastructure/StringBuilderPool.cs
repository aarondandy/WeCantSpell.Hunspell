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

#if !NO_METHODIMPL && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get() => GetClearedBuilder();

        public static StringBuilder Get(string value) =>
            GetClearedBuilderWithCapacity(value.Length).Append(value);

        public static StringBuilder Get(string value, int capacity) =>
            GetClearedBuilderWithCapacity(capacity).Append(value);

#if !NO_METHODIMPL && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get(int capacity) =>
            GetClearedBuilderWithCapacity(capacity);

#if !NO_METHODIMPL && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static StringBuilder Get(string value, int valueStartIndex, int valueLength) =>
            Get(value, valueStartIndex, valueLength, valueLength);

        public static StringBuilder Get(string value, int valueStartIndex, int valueLength, int capacity) =>
            GetClearedBuilderWithCapacity(capacity).Append(value, valueStartIndex, valueLength);

        internal static StringBuilder Get(StringSlice value) =>
            Get(value.Text, value.Offset, value.Length, value.Length);

#if !NO_METHODIMPL && !DEBUG
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

#if !NO_METHODIMPL && !DEBUG
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
            var result = Steal(ref PrimaryCache);
            if (result == null)
            {
                result = Steal(ref SecondaryCache);
                if (result == null)
                {
                    result = Steal(ref TertiaryCache);
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

            var result = PrimaryCache;
            if (result == null || result.Capacity < capacity)
            {
                result = SecondaryCache;
                if (result == null || result.Capacity < capacity)
                {
                    result = TertiaryCache;
                    if (result == null || result.Capacity < capacity)
                    {
                        return new StringBuilder(capacity);
                    }
                    else
                    {
                        TertiaryCache = null;
                    }
                }
                else
                {
                    SecondaryCache = null;
                }
            }
            else
            {
                PrimaryCache = null;
            }

            return result.Clear();
        }

        private static StringBuilder Steal(ref StringBuilder source)
        {
            var taken = source;
            source = null;
            return taken;
        }
    }
}
