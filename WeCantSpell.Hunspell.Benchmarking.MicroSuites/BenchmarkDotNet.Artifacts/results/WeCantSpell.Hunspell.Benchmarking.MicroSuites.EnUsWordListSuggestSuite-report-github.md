``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FIZUWB : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 20.18 ms | 0.371 ms | 0.057 ms |  0.92 |
|             &#39;Suggest root words&#39; | 21.95 ms | 0.247 ms | 0.038 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 22.50 ms | 0.425 ms | 0.281 ms |  1.04 |
|            &#39;Suggest wrong words&#39; | 26.04 ms | 0.279 ms | 0.072 ms |  1.19 |
