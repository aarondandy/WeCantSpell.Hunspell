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
| &#39;Suggest words: WeCantSpell&#39; |   755.0 ms |  5.43 ms | 0.84 ms |   753.7 ms |   755.7 ms |   755.2 ms |  1.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,885.3 ms | 13.29 ms | 0.73 ms | 1,884.5 ms | 1,885.8 ms | 1,885.7 ms |  2.50 |
