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
| &#39;Check words: WeCantSpell&#39; | 18.958 ms | 0.3598 ms | 0.0197 ms | 18.938 ms | 18.977 ms | 18.960 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.165 ms | 0.0718 ms | 0.0111 ms |  6.155 ms |  6.180 ms |  6.162 ms |  0.33 |
