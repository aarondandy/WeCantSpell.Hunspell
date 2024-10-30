using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static partial class StringEx
{
    private const uint Hash1Start = (5381 << 16) + 5381;
    private const uint Hash1StartRotated = ((Hash1Start << 5) | (Hash1Start >> 27)) + Hash1Start;
    private const uint Factor = 1_566_083_941;

#if NO_NUMERICS_BITOPERATIONS

    public static uint GetStableOrdinalHashCode(string value) => value is null ? 0 : GetStableOrdinalHashCode(value.AsSpan());

    public static uint GetStableOrdinalHashCode(ReadOnlySpan<char> value)
    {
        // This is mostly sourced from System.Collections.Frozen.Hashing
        // TODO: replace with non-randomized hashing when that is available
        uint hash1, hash2;

        switch (value.Length)
        {
            case 0:
                return unchecked(Hash1Start * Factor) + Hash1Start;

            case 1:
                return ((value[0] ^ Hash1StartRotated) * Factor) + Hash1Start;

            case 2:
                hash2 = value[0] ^ Hash1StartRotated;
                return (((rotateLeft(hash2) + hash2) ^ value[1]) * Factor) + Hash1Start;

            case 3:
                hash2 = value[0] ^ Hash1StartRotated;
                hash2 = (rotateLeft(hash2) + hash2) ^ value[1];
                return (((rotateLeft(hash2) + hash2) ^ value[2]) * Factor) + Hash1Start;

            default:
                var valueInts = MemoryMarshal.Cast<char, uint>(value);

                switch (value.Length)
                {
                    case 4:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        break;

                    case 5:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash2 = (rotateLeft(hash2) + hash2) ^ value[4];
                        break;

                    case 6:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[2];
                        break;

                    case 7:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[2];
                        hash2 = (rotateLeft(hash2) + hash2) ^ value[6];
                        break;

                    case 8:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[2];
                        hash2 = (rotateLeft(hash2) + hash2) ^ valueInts[3];
                        break;

                    case 9:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[2];
                        hash2 = (rotateLeft(hash2) + hash2) ^ valueInts[3];
                        hash2 = (rotateLeft(hash2) + hash2) ^ value[8];
                        break;

                    case 10:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[2];
                        hash2 = (rotateLeft(hash2) + hash2) ^ valueInts[3];
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[4];
                        break;

                    case 11:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[2];
                        hash2 = (rotateLeft(hash2) + hash2) ^ valueInts[3];
                        hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[4];
                        hash2 = (rotateLeft(hash2) + hash2) ^ value[10];
                        break;

                    default:

                        int i;

                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;

                        for (i = 2; (i + 1) < valueInts.Length; i += 2)
                        {
                            hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[i];
                            hash2 = (rotateLeft(hash2) + hash2) ^ valueInts[i + 1];
                        }

                        if (i < valueInts.Length)
                        {
                            hash1 = (rotateLeft(hash1) + hash1) ^ valueInts[i];
                        }

                        if ((value.Length & 0x01) != 0)
                        {
                            hash2 = (rotateLeft(hash2) + hash2) ^ value[value.Length - 1];
                        }

                        break;
                }

                return (hash2 * Factor) + hash1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static uint rotateLeft(uint value) => (value << 5) | (value >> 27);
    }

#else

    public static uint GetStableOrdinalHashCode(string value) => value is null ? 0 : GetStableOrdinalHashCode(value.AsSpan());

    public static uint GetStableOrdinalHashCode(ReadOnlySpan<char> value)
    {
        // This is mostly sourced from System.Collections.Frozen.Hashing
        // TODO: replace with non-randomized hashing when that is available

        uint hash1, hash2;

        switch (value.Length)
        {
            case 0:
                return unchecked(Hash1Start * Factor) + Hash1Start;

            case 1:
                return ((value[0] ^ Hash1StartRotated) * Factor) + Hash1Start;

            case 2:
                hash2 = value[0] ^ Hash1StartRotated;
                return (((BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[1]) * Factor) + Hash1Start;

            case 3:
                hash2 = value[0] ^ Hash1StartRotated;
                hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[1];
                return (((BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[2]) * Factor) + Hash1Start;

            default:
                var valueInts = MemoryMarshal.Cast<char, uint>(value);

                switch (value.Length)
                {
                    case 4:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        break;

                    case 5:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[4];
                        break;

                    case 6:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[2];
                        break;

                    case 7:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[2];
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[6];
                        break;

                    case 8:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[2];
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ valueInts[3];
                        break;

                    case 9:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[2];
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ valueInts[3];
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[8];
                        break;

                    case 10:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[2];
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ valueInts[3];
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[4];
                        break;

                    case 11:
                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[2];
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ valueInts[3];
                        hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[4];
                        hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[10];
                        break;

                    default:

                        int i;

                        hash1 = valueInts[0] ^ Hash1StartRotated;
                        hash2 = valueInts[1] ^ Hash1StartRotated;

                        for (i = 2; (i + 1) < valueInts.Length; i += 2)
                        {
                            hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[i];
                            hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ valueInts[i + 1];
                        }

                        if (i < valueInts.Length)
                        {
                            hash1 = (BitOperations.RotateLeft(hash1, 5) + hash1) ^ valueInts[i];
                        }

                        if ((value.Length & 0x01) != 0)
                        {
                            hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[value.Length - 1];
                        }

                        break;
                }

                return (hash2 * Factor) + hash1;
        }
    }

#endif

}
