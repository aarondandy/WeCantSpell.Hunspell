```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Check en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

Job=Check en-US  

```
| Method                     | Mean      | Error     | StdDev    | Min       | Median    | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 19.496 ms | 0.0386 ms | 0.0361 ms | 19.448 ms | 19.485 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.324 ms | 0.1178 ms | 0.1902 ms |  6.130 ms |  6.260 ms |  0.33 |
