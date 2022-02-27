``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-EVCJDP : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.70 ms | 0.463 ms | 0.072 ms |  0.86 |
|             &#39;Suggest root words&#39; | 29.73 ms | 0.385 ms | 0.060 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.32 ms | 0.533 ms | 0.317 ms |  0.99 |
|            &#39;Suggest wrong words&#39; | 33.32 ms | 0.435 ms | 0.067 ms |  1.12 |
