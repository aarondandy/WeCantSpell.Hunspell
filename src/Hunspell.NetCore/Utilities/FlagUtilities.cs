using System;
using System.Collections.Generic;
using System.Linq;

namespace Hunspell.Utilities
{
    internal static class FlagUtilities
    {
        public static IEnumerable<int> DecodeFlags(FlagMode flagMode, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return Enumerable.Empty<int>();
            }

            switch (flagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    return parameterText.Select(c => (int)c);
                case FlagMode.Long:
                    return DecodeLongFlags(parameterText);
                case FlagMode.Num:
                    return DecodeNumFlags(parameterText);
                default:
                    throw new NotSupportedException();
            }
        }

        private static IEnumerable<int> DecodeLongFlags(string text)
        {
            if (text == null)
            {
                yield break;
            }

            for (int i = 0; i < text.Length - 1; i += 2)
            {
                yield return unchecked((byte)text[i] << 8 | (byte)text[i + 1]);
            }

            if (text.Length % 2 == 1)
            {
                yield return text[text.Length - 1];
            }
        }

        private static IEnumerable<int> DecodeNumFlags(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Enumerable.Empty<int>();
            }

            return text
                .SplitOnComma()
                .Select(textValue =>
                {
                    int intValue;
                    IntExtensions.TryParseInvariant(textValue, out intValue);
                    return intValue;
                });
        }

        public static bool TryParseFlag(FlagMode flagMode, string text, out int result)
        {
            if (string.IsNullOrEmpty(text))
            {
                result = 0;
                return false;
            }

            switch (flagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    if (text.Length >= 2)
                    {
                        result = MergeCharacterBytes(text[0], text[1]);
                        return true;
                    }

                    result = text[0];
                    return true;
                case FlagMode.Long:
                    if (text.Length >= 2)
                    {
                        result = unchecked(((byte)text[0] << 8) | (byte)text[1]);
                    }
                    else
                    {
                        result = text[0];
                    }

                    return true;
                case FlagMode.Num:
                    return IntExtensions.TryParseInvariant(text, out result);
                default:
                    throw new NotSupportedException();
            }
        }

        public static bool TryParseFlag(FlagMode flagMode, string text, out char result)
        {
            if (string.IsNullOrEmpty(text))
            {
                result = default(char);
                return false;
            }

            switch (flagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    if (text.Length >= 2)
                    {
                        result = (char)MergeCharacterBytes(text[0], text[1]);
                        return true;
                    }

                    result = text[0];
                    return true;
                case FlagMode.Long:
                    if (text.Length >= 2)
                    {
                        result = unchecked((char)(((byte)text[0] << 8) | (byte)text[1]));
                    }
                    else
                    {
                        result = text[0];
                    }

                    return true;
                case FlagMode.Num:
                    return IntExtensions.TryParseInvariantAsChar(text, out result);
                default:
                    throw new NotSupportedException();
            }
        }

        [Obsolete("This method may be able to be replaced by a simple left shift operation like long flags are")]
        private static ushort MergeCharacterBytes(char first, char second)
        {
            return BitConverter.ToUInt16(new byte[] { unchecked((byte)first), unchecked((byte)second) }, 0);
        }
    }
}
