``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TTTCWL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.08 ms | 0.416 ms | 0.023 ms |  0.84 |
|             &#39;Suggest root words&#39; | 29.88 ms | 0.468 ms | 0.208 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.88 ms | 0.332 ms | 0.086 ms |  1.00 |
|            &#39;Suggest wrong words&#39; | 34.61 ms | 0.251 ms | 0.039 ms |  1.15 |
