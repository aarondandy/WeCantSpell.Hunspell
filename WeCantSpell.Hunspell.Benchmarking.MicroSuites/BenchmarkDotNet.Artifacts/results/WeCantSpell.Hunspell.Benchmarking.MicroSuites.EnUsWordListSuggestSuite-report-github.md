``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-XJPVAV : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.86 ms | 0.361 ms | 0.129 ms |  0.92 |
|             &#39;Suggest root words&#39; | 23.71 ms | 0.273 ms | 0.042 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.56 ms | 0.477 ms | 0.170 ms |  1.03 |
|            &#39;Suggest wrong words&#39; | 28.29 ms | 0.374 ms | 0.196 ms |  1.20 |
