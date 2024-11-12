#if NO_HASHCODE

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace System;

struct HashCode
{
    public static int Combine<T0, T1>(T0 value0, T1 value1)
        where T0 : notnull
        where T1 : notnull
    {
        var hash = new HashCode();
        hash.Add(value0);
        hash.Add(value1);
        return hash.ToHashCode();
    }

    public static int Combine<T0, T1, T2>(T0 value0, T1 value1, T2 value2)
        where T0 : notnull
        where T1 : notnull
        where T2 : notnull
    {
        var hash = new HashCode();
        hash.Add(value0);
        hash.Add(value1);
        hash.Add(value2);
        return hash.ToHashCode();
    }

    public HashCode()
    {
        _hash = 24157817;
    }

    private int _hash;

    public void Add<T>(T value) where T : notnull
    {
        _hash = unchecked((_hash * 31) + value.GetHashCode());
    }

    public readonly int ToHashCode() => _hash;
}

#endif
