``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-RXLWXU : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.60 ms | 0.421 ms | 0.220 ms |  0.93 |
|             &#39;Suggest root words&#39; | 23.34 ms | 0.431 ms | 0.112 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.19 ms | 0.365 ms | 0.162 ms |  1.04 |
|            &#39;Suggest wrong words&#39; | 27.75 ms | 0.409 ms | 0.146 ms |  1.19 |
