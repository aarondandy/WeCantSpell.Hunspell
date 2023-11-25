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
| &#39;Check words: WeCantSpell&#39; | 18.937 ms | 0.2954 ms | 0.0457 ms | 18.899 ms | 19.003 ms | 18.924 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  6.152 ms | 0.0939 ms | 0.0145 ms |  6.134 ms |  6.168 ms |  6.153 ms |  0.32 |
