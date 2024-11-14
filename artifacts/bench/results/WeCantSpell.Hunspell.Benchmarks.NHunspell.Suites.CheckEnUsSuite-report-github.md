```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9282.0), X64 RyuJIT VectorSize=256
  Check en-US : .NET Framework 4.8.1 (4.8.9282.0), X64 RyuJIT VectorSize=256

Job=Check en-US  

```
| Method                     | Mean      | Error     | StdDev    | Min       | Median    | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 18.280 ms | 0.0624 ms | 0.0584 ms | 18.200 ms | 18.258 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.018 ms | 0.0168 ms | 0.0149 ms |  6.000 ms |  6.017 ms |  0.33 |
