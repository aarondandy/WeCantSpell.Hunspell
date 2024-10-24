using System.Collections.Generic;

namespace WeCantSpell.Hunspell.Infrastructure;

#if NO_DICTIONARY_GETVALUE || NO_KVP_DECONSTRUCT

static class DictionaryEx
{

#if NO_DICTIONARY_GETVALUE

    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var result) ? result : default;

#endif

#if NO_KVP_DECONSTRUCT

    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
    {
        key = pair.Key;
        value = pair.Value;
    }

#endif

}

#endif
