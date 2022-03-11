``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-IBEHEZ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.52 ms | 0.304 ms | 0.079 ms |  0.83 |
|             &#39;Suggest root words&#39; | 30.85 ms | 0.438 ms | 0.156 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 31.06 ms | 0.434 ms | 0.113 ms |  1.01 |
|            &#39;Suggest wrong words&#39; | 33.53 ms | 0.522 ms | 0.136 ms |  1.09 |
