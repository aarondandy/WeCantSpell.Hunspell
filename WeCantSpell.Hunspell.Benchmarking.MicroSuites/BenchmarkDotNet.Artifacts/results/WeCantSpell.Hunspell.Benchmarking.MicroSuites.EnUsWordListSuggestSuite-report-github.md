``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-BSLIAV : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.78 ms | 0.445 ms | 0.198 ms |  0.98 |
|             &#39;Suggest root words&#39; | 23.30 ms | 0.424 ms | 0.110 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.37 ms | 0.429 ms | 0.190 ms |  1.05 |
|            &#39;Suggest wrong words&#39; | 27.89 ms | 0.525 ms | 0.187 ms |  1.20 |
