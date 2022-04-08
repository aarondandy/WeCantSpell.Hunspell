``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TSRJXZ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 20.37 ms | 0.329 ms | 0.146 ms |  0.89 |
|             &#39;Suggest root words&#39; | 22.84 ms | 0.346 ms | 0.053 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 22.70 ms | 0.397 ms | 0.061 ms |  0.99 |
|            &#39;Suggest wrong words&#39; | 25.69 ms | 0.475 ms | 0.074 ms |  1.12 |
