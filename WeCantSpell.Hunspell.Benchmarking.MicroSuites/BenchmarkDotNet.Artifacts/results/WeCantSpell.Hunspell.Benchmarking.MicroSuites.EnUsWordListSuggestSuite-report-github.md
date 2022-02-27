``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-JNVTFH : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 26.48 ms | 0.294 ms | 0.016 ms |  0.89 |
|             &#39;Suggest root words&#39; | 29.74 ms | 0.409 ms | 0.063 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.48 ms | 0.537 ms | 0.239 ms |  0.99 |
|            &#39;Suggest wrong words&#39; | 33.09 ms | 0.511 ms | 0.079 ms |  1.11 |
