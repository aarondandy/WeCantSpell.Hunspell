using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Mode = {Mode}, Encoding = {Encoding}")]
internal readonly struct FlagParser
{
    internal FlagParser(AffixConfig affix)
        : this(affix.FlagMode, affix.Encoding)
    {
    }

    public FlagParser(FlagParsingMode mode, Encoding encoding)
    {
        Encoding = encoding;
        Mode = mode;
        _flagSetCache = [];

        switch (mode)
        {
            case FlagParsingMode.Char:
                TryParseFlag = FlagValue.TryParseAsChar;
                ParseFlagsInOrder = FlagValue.ParseAsChars;
                ParseFlagSet = FlagSetParseAsChars;
                break;
            case FlagParsingMode.Long:
                TryParseFlag = FlagValue.TryParseAsLong;
                ParseFlagsInOrder = FlagValue.ParseAsLongs;
                ParseFlagSet = FlagSetParseAsLongs;
                break;
            case FlagParsingMode.Num:
                TryParseFlag = FlagValue.TryParseAsNumber;
                ParseFlagsInOrder = FlagValue.ParseAsNumbers;
                ParseFlagSet = FlagSetParseAsNumbers;
                break;
            case FlagParsingMode.Uni:
                if (Encoding.UTF8.Equals(Encoding))
                {
                    goto case FlagParsingMode.Char;
                }
                else
                {
                    TryParseFlag = TryParseFlagWithUnicodeReDecode;
                    ParseFlagsInOrder = ParseFlagsInOrderWithUnicodeReDecode;
                    ParseFlagSet = ParseFlagSetWithUnicodeReDecode;
                }

                break;
            default:
                throwNotSupportedFlagMode();
                TryParseFlag = null!;
                ParseFlagsInOrder = null!;
                ParseFlagSet = null!;
                break;
        }

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void throwNotSupportedFlagMode() => throw new NotSupportedException("Flag mode is not supported");
    }

    public readonly TryParseFlagValueDelegate TryParseFlag;
    public readonly ParseFlagValuesDelegate ParseFlagsInOrder;
    public readonly ParseFlagSetDelegate ParseFlagSet;
    public readonly Encoding Encoding;
    public readonly FlagParsingMode Mode;
    private readonly TextDictionary<FlagSet> _flagSetCache;

    public FlagParser WithEncoding(Encoding encoding) =>
        Encoding.Equals(encoding) ? this : new(Mode, encoding);

    public FlagParser WithMode(FlagParsingMode mode) =>
        Mode == mode ? this : new(mode, Encoding);

    public FlagValue ParseFlagOrDefault(ReadOnlySpan<char> text)
    {
        _ = TryParseFlag(text, out var result);
        return result;
    }

    private readonly bool TryParseFlagWithUnicodeReDecode(ReadOnlySpan<char> text, out FlagValue value) =>
        FlagValue.TryParseAsChar(ReDecodeConvertedStringAsUtf8(text, Encoding), out value);

    private readonly FlagValue[] ParseFlagsInOrderWithUnicodeReDecode(ReadOnlySpan<char> text) =>
        FlagValue.ParseAsChars(ReDecodeConvertedStringAsUtf8(text, Encoding));

    private FlagSet FlagSetParseAsChars(ReadOnlySpan<char> text)
    {
        FlagSet set;
        if (text.Length > 0)
        {
            if (!_flagSetCache.TryGetValue(text, out set))
            {
                var textString = text.ToString();
                set = FlagSet.ParseAsChars(textString);
                _flagSetCache.Add(textString, set);
            }
        }
        else
        {
            set = FlagSet.Empty;
        }

        return set;
    }

    private FlagSet FlagSetParseAsChars(string text)
    {
        FlagSet set;
        if (text.Length > 0)
        {
            if (!_flagSetCache.TryGetValue(text, out set))
            {
                set = FlagSet.ParseAsChars(text);
                _flagSetCache.Add(text, set);
            }
        }
        else
        {
            set = FlagSet.Empty;
        }

        return set;
    }

    private FlagSet FlagSetParseAsLongs(ReadOnlySpan<char> text)
    {
        FlagSet set;
        if (text.Length > 0)
        {
            if (!_flagSetCache.TryGetValue(text, out set))
            {
                set = FlagSet.ParseAsLongs(text);
                _flagSetCache.Add(text.ToString(), set);
            }
        }
        else
        {
            set = FlagSet.Empty;
        }

        return set;
    }

    private FlagSet FlagSetParseAsNumbers(ReadOnlySpan<char> text)
    {
        FlagSet set;
        if (text.Length > 0)
        {
            if (!_flagSetCache.TryGetValue(text, out set))
            {
                set = FlagSet.ParseAsNumbers(text);
                _flagSetCache.Add(text.ToString(), set);
            }
        }
        else
        {
            set = FlagSet.Empty;
        }

        return set;
    }

    private readonly FlagSet ParseFlagSetWithUnicodeReDecode(ReadOnlySpan<char> text) =>
        FlagSetParseAsChars(ReDecodeConvertedStringAsUtf8(text, Encoding));

    private static string ReDecodeConvertedStringAsUtf8(ReadOnlySpan<char> decoded, Encoding encoding)
    {
#if NO_ENCODING_SPANS

        byte[] encodedBytes;
        int encodedBytesCount;

        unsafe
        {
            fixed (char* decodedPointer = &System.Runtime.InteropServices.MemoryMarshal.GetReference(decoded))
            {
                encodedBytes = new byte[Encoding.UTF8.GetByteCount(decodedPointer, decoded.Length)];
                fixed (byte* encodedBytesPointer = &encodedBytes[0])
                {
                    encodedBytesCount = encoding.GetBytes(decodedPointer, decoded.Length, encodedBytesPointer, encodedBytes.Length);
                }
            }
        }

        return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytesCount);
#else
        var buffer = new ArrayBufferWriter<byte>();
        _ = encoding.GetBytes(decoded, buffer);
        return Encoding.UTF8.GetString(buffer.WrittenSpan);
#endif
    }
}
