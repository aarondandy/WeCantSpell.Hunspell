``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-UETZQQ : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 25.36 ms | 0.492 ms | 0.076 ms |  0.86 |    0.00 |
|             &#39;Suggest root words&#39; | 29.49 ms | 0.370 ms | 0.096 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 29.56 ms | 0.540 ms | 0.193 ms |  1.00 |    0.01 |
|            &#39;Suggest wrong words&#39; | 33.81 ms | 0.614 ms | 0.512 ms |  1.15 |    0.02 |
