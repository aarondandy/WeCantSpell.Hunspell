using System;
using System.Text;
using System.Threading;

namespace WeCantSpell.Hunspell.Infrastructure;

static class StringBuilderPool
{
    private const int MaxCachedBuilderCapacity = WordList.MaxWordLen;

    private static StringBuilder? Cache;

    public static StringBuilder Get() => GetClearedBuilder();

    public static StringBuilder Get(int capacity) => GetClearedBuilderWithCapacity(capacity);

    public static StringBuilder Get(string value)
    {
        return GetClearedBuilder().Append(value);
    }

    public static StringBuilder Get(ReadOnlySpan<char> value)
    {
        return GetClearedBuilder().Append(value);
    }

    public static void Return(StringBuilder builder)
    {
        if (builder.Capacity <= MaxCachedBuilderCapacity)
        {
            Volatile.Write(ref Cache, builder);
        }
    }

    public static string GetStringAndReturn(StringBuilder builder)
    {
        var value = builder.ToString();
        Return(builder);
        return value;
    }

    private static StringBuilder GetClearedBuilder()
    {
        var taken = Interlocked.Exchange(ref Cache, null);
        return taken is not null
            ? taken.Clear()
            : new StringBuilder();
    }

    private static StringBuilder GetClearedBuilderWithCapacity(int minimumCapacity)
    {
        var taken = Interlocked.Exchange(ref Cache, null);
        return taken?.Capacity >= minimumCapacity
            ? taken.Clear()
            : new StringBuilder(minimumCapacity);
    }
}
