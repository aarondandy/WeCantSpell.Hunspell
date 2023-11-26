using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace WeCantSpell.Hunspell;

internal struct FlagParser
{
    public FlagParser()
    {
        Mode = FlagParsingMode.Char;
        Encoding = Encoding.UTF8;
    }

    public FlagParser(FlagParsingMode mode, Encoding encoding)
    {
        Mode = mode;
        Encoding = encoding;
    }

    public FlagParsingMode Mode { get; set; }

    public Encoding Encoding { get; set; }

    public readonly FlagValue ParseFlagOrDefault(ReadOnlySpan<char> text)
    {
        _ = TryParseFlag(text, out var result);
        return result;
    }

    public readonly bool TryParseFlag(ReadOnlySpan<char> text, out FlagValue value)
    {
        return Mode switch
        {
            FlagParsingMode.Char => FlagValue.TryParseAsChar(text, out value),
            FlagParsingMode.Uni => TryParseFlagAsUnicode(text, out value),
            FlagParsingMode.Long => FlagValue.TryParseAsLong(text, out value),
            FlagParsingMode.Num => FlagValue.TryParseAsNumber(text, out value),
            _ => noOp(out value)
        };

        static bool noOp(out FlagValue value)
        {
            value = default!;
            return false;
        }
    }

    private readonly bool TryParseFlagAsUnicode(ReadOnlySpan<char> text, out FlagValue value) =>
        FlagValue.TryParseAsChar(ReDecodeConvertedStringAsUtf8(text, Encoding), out value);

    public readonly FlagValue[] ParseFlagsInOrder(ReadOnlySpan<char> text) => Mode switch
    {
        FlagParsingMode.Char => FlagValue.ParseAsChars(text),
        FlagParsingMode.Uni => ParseFlagsInOrderAsUnicode(text),
        FlagParsingMode.Long => FlagValue.ParseAsLongs(text),
        FlagParsingMode.Num => FlagValue.ParseAsNumbers(text),
        _ => ThrowNotSupportedFlagMode<FlagValue[]>()
    };

    private readonly FlagValue[] ParseFlagsInOrderAsUnicode(ReadOnlySpan<char> text) =>
        FlagValue.ParseAsChars(ReDecodeConvertedStringAsUtf8(text, Encoding));

    public readonly FlagSet ParseFlagSet(ReadOnlySpan<char> text) => Mode switch
    {
        FlagParsingMode.Char => FlagSet.ParseAsChars(text),
        FlagParsingMode.Uni => ParseFlagSetAsUnicode(text),
        FlagParsingMode.Long => FlagSet.ParseAsLongs(text),
        FlagParsingMode.Num => FlagSet.ParseAsNumbers(text),
        _ => ThrowNotSupportedFlagMode<FlagSet>()
    };

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
            fixed (char* decodedPointer = &MemoryMarshal.GetReference(decoded))
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

#if !NO_EXPOSED_NULLANNOTATIONS
    [System.Diagnostics.CodeAnalysis.DoesNotReturn]
#endif
    private static TResult ThrowNotSupportedFlagMode<TResult>()
    {
        throw new NotSupportedException("Flag mode is not supported");
    }
}
