``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ZALUUM : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 21.73 ms | 0.381 ms | 0.099 ms |  0.92 |    0.01 |
|             &#39;Suggest root words&#39; | 23.74 ms | 0.441 ms | 0.319 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 24.31 ms | 0.433 ms | 0.258 ms |  1.03 |    0.02 |
|            &#39;Suggest wrong words&#39; | 27.67 ms | 0.394 ms | 0.140 ms |  1.17 |    0.02 |
