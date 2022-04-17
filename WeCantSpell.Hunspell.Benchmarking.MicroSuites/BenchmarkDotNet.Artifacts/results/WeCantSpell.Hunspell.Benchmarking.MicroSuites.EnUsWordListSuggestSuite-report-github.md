``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-UUZVHV : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.66 ms | 0.349 ms | 0.124 ms |  0.92 |
|             &#39;Suggest root words&#39; | 23.56 ms | 0.396 ms | 0.061 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.36 ms | 0.413 ms | 0.183 ms |  1.08 |
|            &#39;Suggest wrong words&#39; | 28.25 ms | 0.542 ms | 0.084 ms |  1.20 |
