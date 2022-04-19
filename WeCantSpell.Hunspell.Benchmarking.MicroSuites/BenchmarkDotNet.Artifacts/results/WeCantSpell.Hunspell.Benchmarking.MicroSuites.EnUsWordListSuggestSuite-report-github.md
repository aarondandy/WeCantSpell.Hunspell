``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ATICDS : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.39 ms | 0.359 ms | 0.160 ms |  0.91 |
|             &#39;Suggest root words&#39; | 23.52 ms | 0.321 ms | 0.083 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.64 ms | 0.378 ms | 0.168 ms |  1.05 |
|            &#39;Suggest wrong words&#39; | 28.24 ms | 0.314 ms | 0.112 ms |  1.20 |
