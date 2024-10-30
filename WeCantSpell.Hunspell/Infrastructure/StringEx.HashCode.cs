using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WeCantSpell.Hunspell.Infrastructure;

static partial class StringEx
{
    private const uint Hash1Start = (5381 << 16) + 5381;
    private const uint Hash1StartRotated =  ((Hash1Start << 5) | (Hash1Start >> (32 - 5))) + Hash1Start;
    private const uint Factor = 1_566_083_941;

    public static uint GetStableOrdinalHashCode(string value) => value is null ? 0 : GetStableOrdinalHashCode(value.AsSpan());

    public static uint GetStableOrdinalHashCode(ReadOnlySpan<char> value)
    {
        // This is mostly sourced from System.Collections.Frozen.Hashing
        // TODO: replace with non-randomized hashing when that is available

        uint hash1, hash2;

        switch (value.Length)
        {
            case 0:
                return (Hash1Start + unchecked(Hash1Start * Factor));

            case 1:
                hash2 = value[0] ^ Hash1StartRotated;
                goto tinyResult;

            case 2:
                hash2 = value[0] ^ Hash1StartRotated;
                hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[1];
                goto tinyResult;

            case 3:
                hash2 = value[0] ^ Hash1StartRotated;
                hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[1];
                hash2 = (BitOperations.RotateLeft(hash2, 5) + hash2) ^ value[2];

            tinyResult:
                return Hash1Start + (hash2 * Factor);

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

                return hash1 + (hash2 * Factor);
        }
    }
}
