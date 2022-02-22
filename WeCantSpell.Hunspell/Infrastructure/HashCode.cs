namespace WeCantSpell.Hunspell.Infrastructure;

#if NO_HASHCODE

static class HashCode
{
    public static int Combine<T>(int value0, T value1) => value1 switch
    {
        null => value0,
        _ => Combine(value0, value1.GetHashCode())
    };

    public static int Combine(int value0, int value1) => unchecked((value0 * 31) + value1);

    public static int Combine<T>(int value0, T value1, T value2) => Combine(Combine(value0, value1), value2);

    public static int Combine<T>(int value0, T value1, T value2, T value3) => Combine(Combine(Combine(value0, value1), value2), value3);
}

#endif
