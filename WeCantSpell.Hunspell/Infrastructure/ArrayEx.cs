namespace WeCantSpell.Hunspell.Infrastructure;

static class ArrayEx<T>
{
#if NO_ARRAY_EMPTY
    internal static readonly T[] Empty = new T[0];
#else
    internal static readonly T[] Empty = System.Array.Empty<T>();
#endif
}
