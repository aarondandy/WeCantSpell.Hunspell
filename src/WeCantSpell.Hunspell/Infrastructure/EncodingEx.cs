using System;
using System.Text;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class EncodingEx
    {
        public static Encoding GetEncodingByName(string encodingName)
        {
            if (string.IsNullOrEmpty(encodingName))
            {
                return null;
            }

            if ("UTF-8".Equals(encodingName, StringComparison.OrdinalIgnoreCase) || "UTF8".Equals(encodingName, StringComparison.OrdinalIgnoreCase))
            {
                return Encoding.UTF8;
            }

            try
            {
                return Encoding.GetEncoding(encodingName);
            }
            catch (ArgumentException)
            {
                return GetEncodingByAlternateNames(encodingName);
            }
        }

        private static Encoding GetEncodingByAlternateNames(string encodingName)
        {
            var spaceIndex = encodingName.IndexOf(' ');
            if (spaceIndex > 0)
            {
                return GetEncodingByName(encodingName.Substring(0, spaceIndex));
            }

            if (encodingName.Length >= 4 && encodingName.StartsWith("ISO") && encodingName[3] != '-')
            {
                return GetEncodingByName(encodingName.Insert(3, "-"));
            }

            return null;
        }
    }
}
