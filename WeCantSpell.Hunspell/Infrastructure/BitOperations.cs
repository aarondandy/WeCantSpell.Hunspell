using System.Runtime.CompilerServices;

#if NO_NUMERICS

namespace System.Numerics;

internal static class BitOperations
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint RotateLeft(uint value, int offset) => (value << offset) | (value >> (32 - offset));
}

#endif
