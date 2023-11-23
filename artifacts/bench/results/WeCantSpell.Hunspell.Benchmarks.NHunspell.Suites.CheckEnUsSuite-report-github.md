```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-TPWOKF : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

MinInvokeCount=1  IterationTime=1.0000 s  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
| Method                     | Mean      | Error     | StdDev    | Min       | Max       | Median    | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 18.521 ms | 0.1870 ms | 0.0289 ms | 18.490 ms | 18.558 ms | 18.519 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.126 ms | 0.0670 ms | 0.0104 ms |  6.113 ms |  6.138 ms |  6.127 ms |  0.33 |
