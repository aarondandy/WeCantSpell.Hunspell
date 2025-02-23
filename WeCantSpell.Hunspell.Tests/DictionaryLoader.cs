using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;

namespace WeCantSpell.Hunspell.Tests;

internal class DictionaryLoader
{
    static DictionaryLoader()
    {
        Helpers.EnsureEncodingsReady();
        _cache = new(new MemoryCacheOptions()
        {
            SizeLimit = 100
        });
    }

    private static readonly MemoryCache _cache;

    internal static Task<WordList> GetDictionaryAsync(string filePath, CancellationToken ct)
    {
        return _cache.GetOrCreate(filePath, e =>
        {
            e.Size = 1;
            e.SlidingExpiration = TimeSpan.FromSeconds(30);
            return WordList.CreateFromFilesAsync(filePath, ct);
        });
    }
}
