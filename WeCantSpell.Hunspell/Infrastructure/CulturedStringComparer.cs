using System;
using System.Globalization;

namespace WeCantSpell.Hunspell.Infrastructure;

/// <summary>
/// Provides the ability to compare text using a configured culture.
/// </summary>
sealed class CulturedStringComparer : StringComparer
{
    public CulturedStringComparer(CultureInfo culture)
    {
        if (culture is null) throw new ArgumentNullException(nameof(culture));

        _compareInfo = culture.CompareInfo;
    }

    private readonly CompareInfo _compareInfo;

    public override int Compare(string x, string y) => _compareInfo.Compare(x, y);

    public override bool Equals(string x, string y) => _compareInfo.Compare(x, y) == 0;

    public override int GetHashCode(string obj)
    {
#if NO_COMPAREINFO_HASHCODE
        return 0;
#else
        return _compareInfo.GetHashCode(obj, CompareOptions.None);
#endif
    }
}
