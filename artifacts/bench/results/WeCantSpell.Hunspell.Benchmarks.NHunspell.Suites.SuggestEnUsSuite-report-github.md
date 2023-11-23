```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-TPWOKF : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

MinInvokeCount=1  IterationTime=1.0000 s  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
| Method                       | Mean       | Error    | StdDev   | Min        | Max        | Median     | Ratio | RatioSD |
|----------------------------- |-----------:|---------:|---------:|-----------:|-----------:|-----------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; |   759.0 ms |  7.36 ms |  1.14 ms |   758.1 ms |   760.7 ms |   758.7 ms |  1.00 |    0.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,895.0 ms | 32.72 ms | 17.11 ms | 1,873.2 ms | 1,911.9 ms | 1,899.5 ms |  2.50 |    0.02 |
