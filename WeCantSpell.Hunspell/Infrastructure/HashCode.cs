﻿namespace WeCantSpell.Hunspell.Infrastructure;

#if NO_HASHCODE

struct HashCode
{
    public static int Combine<T0, T1>(T0 value0, T1 value1) where T0 : notnull where T1 : notnull
    {
        var hash = new HashCode();
        hash.Add(value0);
        hash.Add(value1);
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

    public int ToHashCode() => _hash;
}

#endif
