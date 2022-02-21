``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-ATTJGT : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.09 ms | 0.468 ms | 0.121 ms |  0.83 |
|             &#39;Suggest root words&#39; | 30.18 ms | 0.273 ms | 0.042 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 31.24 ms | 0.327 ms | 0.051 ms |  1.04 |
|            &#39;Suggest wrong words&#39; | 33.36 ms | 0.525 ms | 0.081 ms |  1.11 |
