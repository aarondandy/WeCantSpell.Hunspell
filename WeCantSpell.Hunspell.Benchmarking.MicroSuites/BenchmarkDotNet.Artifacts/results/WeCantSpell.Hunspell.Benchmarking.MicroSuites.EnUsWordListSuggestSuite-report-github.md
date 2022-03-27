``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-KEZYEW : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 23.20 ms | 0.425 ms | 0.189 ms |  0.94 |
|             &#39;Suggest root words&#39; | 24.76 ms | 0.404 ms | 0.179 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.89 ms | 0.414 ms | 0.217 ms |  1.05 |
|            &#39;Suggest wrong words&#39; | 29.52 ms | 0.520 ms | 0.080 ms |  1.19 |
