```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Check en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

Job=Check en-US  

```
| Method                     | Mean      | Error     | StdDev    | Median    | Min       | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 18.629 ms | 0.0472 ms | 0.0442 ms | 18.629 ms | 18.550 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.299 ms | 0.1252 ms | 0.2413 ms |  6.168 ms |  6.113 ms |  0.35 |
