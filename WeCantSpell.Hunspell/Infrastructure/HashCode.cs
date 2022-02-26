namespace WeCantSpell.Hunspell.Infrastructure;

#if NO_HASHCODE

static class HashCode
{
    public static int Combine<T>(int value0, T value1) where T : notnull => Combine(value0, value1.GetHashCode());

    public static int Combine(int value0, int value1) => unchecked((value0 * 31) + value1);
}

#endif
