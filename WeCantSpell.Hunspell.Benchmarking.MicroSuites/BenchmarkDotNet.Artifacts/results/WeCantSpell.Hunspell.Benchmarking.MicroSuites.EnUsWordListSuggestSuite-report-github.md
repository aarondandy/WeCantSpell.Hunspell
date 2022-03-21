``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-GMUPIW : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.49 ms | 0.309 ms | 0.080 ms |  0.92 |
|             &#39;Suggest root words&#39; | 24.32 ms | 0.515 ms | 0.080 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.67 ms | 0.395 ms | 0.141 ms |  1.01 |
|            &#39;Suggest wrong words&#39; | 28.92 ms | 0.527 ms | 0.276 ms |  1.19 |
