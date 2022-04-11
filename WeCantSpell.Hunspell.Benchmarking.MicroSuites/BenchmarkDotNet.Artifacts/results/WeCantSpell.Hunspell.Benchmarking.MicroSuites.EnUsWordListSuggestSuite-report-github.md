``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-HENIXT : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.65 ms | 0.388 ms | 0.138 ms |  0.93 |
|             &#39;Suggest root words&#39; | 24.39 ms | 0.372 ms | 0.133 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 26.05 ms | 0.393 ms | 0.140 ms |  1.07 |
|            &#39;Suggest wrong words&#39; | 28.68 ms | 0.537 ms | 0.355 ms |  1.18 |
