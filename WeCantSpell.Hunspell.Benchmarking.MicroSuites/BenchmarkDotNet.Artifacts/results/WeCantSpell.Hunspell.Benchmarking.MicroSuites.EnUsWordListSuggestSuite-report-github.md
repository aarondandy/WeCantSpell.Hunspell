``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FKUIZY : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 22.05 ms | 0.346 ms | 0.090 ms |  0.92 |    0.01 |
|             &#39;Suggest root words&#39; | 24.21 ms | 0.477 ms | 0.316 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 24.94 ms | 0.451 ms | 0.298 ms |  1.03 |    0.02 |
|            &#39;Suggest wrong words&#39; | 28.09 ms | 0.467 ms | 0.072 ms |  1.17 |    0.01 |
