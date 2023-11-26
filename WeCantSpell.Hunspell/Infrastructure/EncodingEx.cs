using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WeCantSpell.Hunspell.Infrastructure;

static class EncodingEx
{
    public static Encoding? GetEncodingByName(string encodingName) =>
        GetUtf8EncodingOrDefault(encodingName.AsSpan()) ?? GetEncodingFromDatabase(encodingName);

    public static Encoding? GetEncodingByName(ReadOnlySpan<char> encodingName) =>
        GetUtf8EncodingOrDefault(encodingName) ?? GetEncodingFromDatabase(encodingName.ToString());

    private static Encoding? GetUtf8EncodingOrDefault(ReadOnlySpan<char> encodingName)
    {
        if (encodingName.EqualsOrdinal("UTF8") || encodingName.EqualsOrdinal("UTF-8"))
        {
            return Encoding.UTF8;
        }

        return null;
    }

    private static Encoding? GetEncodingFromDatabase(string encodingName)
    {
        try
        {
            return Encoding.GetEncoding(encodingName);
        }
        catch (ArgumentException)
        {
            return getEncodingByAlternateNames(encodingName);
        }

        static Encoding? getEncodingByAlternateNames(string encodingName)
        {
            var spaceIndex = encodingName.IndexOf(' ');
            if (spaceIndex > 0)
            {
                return GetEncodingByName(encodingName.AsSpan(0, spaceIndex));
            }

            if (encodingName.Length >= 4 && encodingName.StartsWith("ISO") && encodingName[3] != '-')
            {
                return GetEncodingByName(encodingName.Insert(3, "-"));
            }

            return null;
        }
    }

#if NO_SPAN_DECODE
    public static void Convert(this Decoder decoder, ReadOnlySpan<byte> bytes, Span<char> chars, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
    {
        unsafe
        {
            fixed (byte* bytesPointer = &MemoryMarshal.GetReference(bytes))
            fixed (char* charsPointer = &MemoryMarshal.GetReference(chars))
            {
                decoder.Convert(
                    bytesPointer,
                    bytes.Length,
                    charsPointer,
                    chars.Length,
                    flush: false,
                    out bytesUsed,
                    out charsUsed,
                    out completed);
            }
        }
    }
#endif
}
