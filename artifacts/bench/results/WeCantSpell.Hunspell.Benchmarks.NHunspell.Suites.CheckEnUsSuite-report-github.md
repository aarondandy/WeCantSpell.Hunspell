```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9277.0), X64 RyuJIT VectorSize=256
  Check en-US : .NET Framework 4.8.1 (4.8.9277.0), X64 RyuJIT VectorSize=256

Job=Check en-US  

```
| Method                     | Mean      | Error     | StdDev    | Min       | Median    | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 18.268 ms | 0.0344 ms | 0.0322 ms | 18.228 ms | 18.257 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.121 ms | 0.0075 ms | 0.0067 ms |  6.106 ms |  6.121 ms |  0.34 |
