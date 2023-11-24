```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-TPWOKF : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

MinInvokeCount=1  IterationTime=1.0000 s  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
| Method                       | Mean       | Error    | StdDev  | Min        | Max        | Median     | Ratio |
|----------------------------- |-----------:|---------:|--------:|-----------:|-----------:|-----------:|------:|
| &#39;Suggest words: WeCantSpell&#39; |   759.5 ms |  7.59 ms | 1.17 ms |   757.7 ms |   760.3 ms |   759.9 ms |  1.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,917.1 ms | 36.89 ms | 5.71 ms | 1,908.6 ms | 1,920.6 ms | 1,919.7 ms |  2.52 |
