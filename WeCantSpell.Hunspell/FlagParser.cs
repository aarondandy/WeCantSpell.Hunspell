using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeCantSpell.Hunspell;

internal struct FlagParser
{
    public FlagParser(FlagParsingMode mode, Encoding encoding)
    {
        _encoding = encoding;
        _tryParseValue = FlagValue.TryParseAsChar;
        _parseSet = FlagSet.ParseAsChars;
        _parseValues = FlagValue.ParseAsChars;
        Mode = mode;
    }

    private Encoding _encoding;
    private TryParseValueDelegate _tryParseValue;
    private ParseValuesDelegate _parseValues;
    private ParseSetDelegate _parseSet;
    private FlagParsingMode _mode;

    public Encoding Encoding
    {
        readonly get => _encoding;
        set => _encoding = value;
    }

    public FlagParsingMode Mode
    {
        readonly get => _mode;
        set
        {
            switch (value)
            {
                case FlagParsingMode.Char:
                    _tryParseValue = FlagValue.TryParseAsChar;
                    _parseValues = FlagValue.ParseAsChars;
                    _parseSet = FlagSet.ParseAsChars;
                    break;
                case FlagParsingMode.Long:
                    _tryParseValue = FlagValue.TryParseAsLong;
                    _parseValues = FlagValue.ParseAsLongs;
                    _parseSet = FlagSet.ParseAsLongs;
                    break;
                case FlagParsingMode.Num:
                    _tryParseValue = FlagValue.TryParseAsNumber;
                    _parseValues = FlagValue.ParseAsNumbers;
                    _parseSet = FlagSet.ParseAsNumbers;
                    break;
                case FlagParsingMode.Uni:
                    _tryParseValue = TryParseFlagAsUnicode;
                    _parseValues = ParseFlagsInOrderAsUnicode;
                    _parseSet = ParseFlagSetAsUnicode;
                    break;
                default:
                    throwNotSupportedFlagMode();
                    break;
            }

            _mode = value;

#if !NO_EXPOSED_NULLANNOTATIONS
            [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
            [MethodImpl(MethodImplOptions.NoInlining)]
            static void throwNotSupportedFlagMode() => throw new NotSupportedException("Flag mode is not supported");
        }
    }

    public readonly FlagValue ParseFlagOrDefault(ReadOnlySpan<char> text)
    {
        _ = TryParseFlag(text, out var result);
        return result;
    }

    public readonly bool TryParseFlag(ReadOnlySpan<char> text, out FlagValue value) => _tryParseValue(text, out value);

    private readonly bool TryParseFlagAsUnicode(ReadOnlySpan<char> text, out FlagValue value) =>
        FlagValue.TryParseAsChar(ReDecodeConvertedStringAsUtf8(text, Encoding), out value);

    public readonly FlagValue[] ParseFlagsInOrder(ReadOnlySpan<char> text) => _parseValues(text);

    private readonly FlagValue[] ParseFlagsInOrderAsUnicode(ReadOnlySpan<char> text) =>
        FlagValue.ParseAsChars(ReDecodeConvertedStringAsUtf8(text, Encoding));

    public readonly FlagSet ParseFlagSet(ReadOnlySpan<char> text) => _parseSet(text);

    private readonly FlagSet ParseFlagSetAsUnicode(ReadOnlySpan<char> text) =>
        FlagSet.ParseAsChars(ReDecodeConvertedStringAsUtf8(text, Encoding));

    private static ReadOnlySpan<char> ReDecodeConvertedStringAsUtf8(ReadOnlySpan<char> decoded, Encoding encoding)
    {
        if (Encoding.UTF8.Equals(encoding))
        {
            return decoded;
        }

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

        return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytesCount).AsSpan();
#else
        var buffer = new ArrayBufferWriter<byte>();
        _ = encoding.GetBytes(decoded, buffer);
        return Encoding.UTF8.GetString(buffer.WrittenSpan).AsSpan();
#endif
    }

    private delegate bool TryParseValueDelegate(ReadOnlySpan<char> text, out FlagValue value);
    private delegate FlagSet ParseSetDelegate(ReadOnlySpan<char> text);
    private delegate FlagValue[] ParseValuesDelegate(ReadOnlySpan<char> text);
}
