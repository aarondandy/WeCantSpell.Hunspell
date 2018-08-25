using System;
using System.Text;

namespace WeCantSpell.Hunspell.Infrastructure
{
    static class EncodingEx
    {
        private const string Utf8Name = "UTF8";

        private const string Utf8NameWithDash = "UTF-8";

        public static Encoding GetEncodingByName(ReadOnlySpan<char> encodingName)
        {
            if (encodingName.IsEmpty)
            {
                return null;
            }

            if (encodingName.Equals(Utf8NameWithDash.AsSpan(), StringComparison.OrdinalIgnoreCase) || encodingName.Equals(Utf8Name.AsSpan(), StringComparison.OrdinalIgnoreCase))
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

        private static Encoding GetEncodingByAlternateNames(string encodingName)
        {
            var spaceIndex = encodingName.IndexOf(' ');
            if (spaceIndex > 0)
            {
                return GetEncodingByName(encodingName.AsSpan(0, spaceIndex));
            }

            if (encodingName.Length >= 4 && encodingName.StartsWith("ISO") && encodingName[3] != '-')
            {
                return GetEncodingByName(encodingName.Insert(3, "-").AsSpan());
            }

            return null;
        }
    }
}
