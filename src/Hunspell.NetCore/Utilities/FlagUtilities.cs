using System;
using System.Collections.Generic;
using System.Linq;

namespace Hunspell.Utilities
{
    internal static class FlagUtilities
    {
        public static IEnumerable<FlagValue> DecodeFlags(FlagMode flagMode, string parameterText)
        {
            if (string.IsNullOrEmpty(parameterText))
            {
                return Enumerable.Empty<FlagValue>();
            }

            switch (flagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    return parameterText.Select(c => new FlagValue(c));
                case FlagMode.Long:
                    return DecodeLongFlags(parameterText);
                case FlagMode.Num:
                    return DecodeNumFlags(parameterText);
                default:
                    throw new NotSupportedException();
            }
        }

        private static IEnumerable<FlagValue> DecodeLongFlags(string text)
        {
            if (text == null)
            {
                yield break;
            }

            for (int i = 0; i < text.Length - 1; i += 2)
            {
                yield return new FlagValue(text[i], unchecked((byte)text[i + 1]));
            }

            if (text.Length % 2 == 1)
            {
                yield return new FlagValue(text[text.Length - 1]);
            }
        }

        private static IEnumerable<FlagValue> DecodeNumFlags(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Enumerable.Empty<FlagValue>();
            }

            return text
                .SplitOnComma()
                .Select(textValue =>
                {
                    FlagValue flagValue;
                    FlagValue.TryParse(textValue, out flagValue);
                    return flagValue;
                });
        }

        public static bool TryParseFlag(FlagMode flagMode, string text, out FlagValue result)
        {
            if (string.IsNullOrEmpty(text))
            {
                result = default(FlagValue);
                return false;
            }

            switch (flagMode)
            {
                case FlagMode.Char:
                case FlagMode.Uni:
                    if (text.Length >= 2)
                    {
                        result = new FlagValue(MergeCharacterBytes(text[0], text[1]));
                        return true;
                    }

                    result = new FlagValue(text[0]);
                    return true;
                case FlagMode.Long:
                    if (text.Length >= 2)
                    {
                        result = new FlagValue(text[0], unchecked((byte)text[1]));
                    }
                    else
                    {
                        result = new FlagValue(text[0]);
                    }

                    return true;
                case FlagMode.Num:
                    return FlagValue.TryParse(text, out result);
                default:
                    throw new NotSupportedException();
            }
        }

        [Obsolete]
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
