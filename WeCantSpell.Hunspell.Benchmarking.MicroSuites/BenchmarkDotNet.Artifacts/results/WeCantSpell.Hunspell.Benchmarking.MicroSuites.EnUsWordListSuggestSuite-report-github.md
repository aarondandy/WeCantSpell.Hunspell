``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FOVRWU : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 26.23 ms | 0.295 ms | 0.046 ms |  0.86 |
|             &#39;Suggest root words&#39; | 30.61 ms | 0.373 ms | 0.097 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 30.08 ms | 0.293 ms | 0.016 ms |  0.98 |
|            &#39;Suggest wrong words&#39; | 33.73 ms | 0.535 ms | 0.191 ms |  1.10 |
