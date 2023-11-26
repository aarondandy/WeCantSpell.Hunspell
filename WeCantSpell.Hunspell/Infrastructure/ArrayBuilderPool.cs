using System.Threading;

namespace WeCantSpell.Hunspell.Infrastructure;

static class ArrayBuilderPool<T>
{
    private static ArrayBuilder<T>? Cache;

    public static ArrayBuilder<T> Get()
    {
        if (Interlocked.Exchange(ref Cache, null) is { } builder)
        {
            builder.Clear();
        }
        else
        {
            builder = new();
        }

        return builder;
    }

    public static T[] ExtractAndReturn(ArrayBuilder<T> rental)
    {
        var result = rental.Extract();
        Return(rental);
        return result;
    }

    public static void Return(ArrayBuilder<T> rental)
    {
        Volatile.Write(ref Cache, rental);
    }
}
