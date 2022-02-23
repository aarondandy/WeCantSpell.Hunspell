using System.Collections.Generic;
using System.Linq;

namespace WeCantSpell.Hunspell.Infrastructure;

static class CollectionsEx
{

#if NO_DICTIONARY_GETVALUE

    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var result) ? result : default;

    public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) =>
        dictionary.TryGetValue(key, out var result) ? result : defaultValue;

#endif

    public static IEnumerable<TValue> WhereNotNull<TValue>(this IEnumerable<TValue?> values) where TValue : class=>
        values.Where<TValue>(static value => value is not null);

}
