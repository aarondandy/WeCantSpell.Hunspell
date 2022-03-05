using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WeCantSpell.Hunspell.Infrastructure;

static class EncodingEx
{
    public static Encoding? GetEncodingByName(string encodingName) => GetEncodingByName(encodingName.AsSpan());

    public static Encoding? GetEncodingByName(ReadOnlySpan<char> encodingName)
    {
        if (encodingName.IsEmpty)
        {
            return null;
        }

        if (encodingName.Equals("UTF8", StringComparison.OrdinalIgnoreCase) || encodingName.Equals("UTF-8", StringComparison.OrdinalIgnoreCase))
        {
            return Encoding.UTF8;
        }

        var encodingNameString = encodingName.ToString();
        try
        {
            return Encoding.GetEncoding(encodingNameString);
        }
        catch (ArgumentException)
        {
            return GetEncodingByAlternateNames(encodingNameString);
        }
    }

    private static Encoding? GetEncodingByAlternateNames(string encodingName)
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
