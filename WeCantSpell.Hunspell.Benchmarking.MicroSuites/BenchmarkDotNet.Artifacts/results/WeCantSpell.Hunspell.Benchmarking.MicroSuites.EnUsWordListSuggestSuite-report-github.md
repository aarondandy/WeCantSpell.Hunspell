``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-PXVEID : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.91 ms | 0.394 ms | 0.102 ms |  0.84 |
|             &#39;Suggest root words&#39; | 30.72 ms | 0.446 ms | 0.116 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 30.33 ms | 0.344 ms | 0.205 ms |  0.98 |
|            &#39;Suggest wrong words&#39; | 33.75 ms | 0.362 ms | 0.094 ms |  1.10 |
