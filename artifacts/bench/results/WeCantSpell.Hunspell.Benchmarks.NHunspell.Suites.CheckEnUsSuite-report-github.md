```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
AMD Ryzen 7 5800H with Radeon Graphics 3.20GHz, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  Check en-US : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256

Job=Check en-US  

```
| Method                     | Mean      | Error     | StdDev    | Min       | Median    | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 18.283 ms | 0.0397 ms | 0.0371 ms | 18.209 ms | 18.294 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.010 ms | 0.0079 ms | 0.0066 ms |  5.995 ms |  6.010 ms |  0.33 |
