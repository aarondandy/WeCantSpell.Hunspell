``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-ZNMMNF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 24.81 ms | 0.428 ms | 0.152 ms |  1.00 |
|             &#39;Suggest root words&#39; | 24.75 ms | 0.412 ms | 0.147 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.99 ms | 0.508 ms | 0.336 ms |  1.05 |
|            &#39;Suggest wrong words&#39; | 29.96 ms | 0.556 ms | 0.198 ms |  1.21 |
