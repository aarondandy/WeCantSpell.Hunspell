```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3194)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9290.0), X64 RyuJIT VectorSize=256
  Check en-US : .NET Framework 4.8.1 (4.8.9290.0), X64 RyuJIT VectorSize=256

Job=Check en-US  

```
| Method                     | Mean      | Error     | StdDev    | Min       | Median    | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 19.807 ms | 0.0601 ms | 0.0562 ms | 19.677 ms | 19.803 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  5.969 ms | 0.0083 ms | 0.0077 ms |  5.956 ms |  5.967 ms |  0.30 |
