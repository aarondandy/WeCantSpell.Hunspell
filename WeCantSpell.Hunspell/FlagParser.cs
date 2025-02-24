using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeCantSpell.Hunspell;

[DebuggerDisplay("Mode = {Mode}, Encoding = {Encoding}")]
internal readonly struct FlagParser
{
    public FlagParser(FlagParsingMode mode, Encoding encoding)
    {
        _encoding = encoding;
        _mode = mode;

        switch (mode)
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
                if (Encoding.UTF8.Equals(_encoding))
                {
                    goto case FlagParsingMode.Char;
                }
                else
                {
                    _tryParseValue = TryParseFlagWithUnicodeReDecode;
                    _parseValues = ParseFlagsInOrderWithUnicodeReDecode;
                    _parseSet = ParseFlagSetWithUnicodeReDecode;
                }

                break;
            default:
                throwNotSupportedFlagMode();
                _tryParseValue = null!;
                _parseValues = null!;
                _parseSet = null!;
                break;
        }

#if !NO_EXPOSED_NULLANNOTATIONS
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void throwNotSupportedFlagMode() => throw new NotSupportedException("Flag mode is not supported");
    }

    private readonly TryParseValueDelegate _tryParseValue;
    private readonly ParseValuesDelegate _parseValues;
    private readonly ParseSetDelegate _parseSet;
    private readonly Encoding _encoding;
    private readonly FlagParsingMode _mode;

    public Encoding Encoding => _encoding;

    public FlagParsingMode Mode => _mode;

    public FlagParser WithEncoding(Encoding encoding) => new(_mode, encoding);

    public FlagParser WithMode(FlagParsingMode mode) => new(mode, _encoding);

    public FlagValue ParseFlagOrDefault(ReadOnlySpan<char> text)
    {
        _ = TryParseFlag(text, out var result);
        return result;
    }

    public readonly bool TryParseFlag(ReadOnlySpan<char> text, out FlagValue value) => _tryParseValue(text, out value);

    private readonly bool TryParseFlagWithUnicodeReDecode(ReadOnlySpan<char> text, out FlagValue value) =>
        FlagValue.TryParseAsChar(ReDecodeConvertedStringAsUtf8(text, _encoding), out value);

    public readonly FlagValue[] ParseFlagsInOrder(ReadOnlySpan<char> text) => _parseValues(text);

    private readonly FlagValue[] ParseFlagsInOrderWithUnicodeReDecode(ReadOnlySpan<char> text) =>
        FlagValue.ParseAsChars(ReDecodeConvertedStringAsUtf8(text, _encoding));

    public readonly FlagSet ParseFlagSet(ReadOnlySpan<char> text) => _parseSet(text);

    private readonly FlagSet ParseFlagSetWithUnicodeReDecode(ReadOnlySpan<char> text) =>
        FlagSet.ParseAsChars(ReDecodeConvertedStringAsUtf8(text, _encoding));

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

    private delegate bool TryParseValueDelegate(ReadOnlySpan<char> text, out FlagValue value);

    private delegate FlagSet ParseSetDelegate(ReadOnlySpan<char> text);

    private delegate FlagValue[] ParseValuesDelegate(ReadOnlySpan<char> text);
}
