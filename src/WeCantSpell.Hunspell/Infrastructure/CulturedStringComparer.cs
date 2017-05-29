using System;
using System.Globalization;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal sealed class CulturedStringComparer : StringComparer
    {
        public CulturedStringComparer(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            Culture = culture;
            CompareInfo = culture.CompareInfo;
        }

        private CultureInfo Culture { get; }

        private CompareInfo CompareInfo { get; }

        public override int Compare(string x, string y) => CompareInfo.Compare(x, y);

        public override bool Equals(string x, string y) => Compare(x, y) == 0;

        public override int GetHashCode(string obj)
        {
#if NO_COMPAREINFO_HASHCODE
            return 0;
#else
            return CompareInfo.GetHashCode(obj, CompareOptions.None);
#endif
        }
    }
}
