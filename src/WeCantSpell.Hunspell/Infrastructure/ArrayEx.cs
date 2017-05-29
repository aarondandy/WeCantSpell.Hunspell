namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class ArrayEx<T>
    {
#if NO_ARRAY_EMPTY
        public static readonly T[] Empty = new T[0];
#else
        public static readonly T[] Empty = System.Array.Empty<T>();
#endif
    }
}
