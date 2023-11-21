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
| &#39;Check words: WeCantSpell&#39; | 19.080 ms | 0.3218 ms | 0.0498 ms | 19.030 ms | 19.132 ms | 19.078 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.113 ms | 0.1177 ms | 0.0065 ms |  6.106 ms |  6.119 ms |  6.114 ms |  0.32 |
