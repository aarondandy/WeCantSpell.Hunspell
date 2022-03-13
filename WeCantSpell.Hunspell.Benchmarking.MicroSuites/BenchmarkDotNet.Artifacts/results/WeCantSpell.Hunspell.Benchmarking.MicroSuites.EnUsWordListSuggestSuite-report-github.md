``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-QSOGKR : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.15 ms | 0.435 ms | 0.288 ms |  0.90 |
|             &#39;Suggest root words&#39; | 24.58 ms | 0.385 ms | 0.100 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.83 ms | 0.443 ms | 0.197 ms |  1.01 |
|            &#39;Suggest wrong words&#39; | 27.73 ms | 0.509 ms | 0.028 ms |  1.13 |
