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
| &#39;Check words: WeCantSpell&#39; | 18.937 ms | 0.2733 ms | 0.0423 ms | 18.889 ms | 18.989 ms | 18.934 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.146 ms | 0.0624 ms | 0.0097 ms |  6.136 ms |  6.157 ms |  6.146 ms |  0.32 |
