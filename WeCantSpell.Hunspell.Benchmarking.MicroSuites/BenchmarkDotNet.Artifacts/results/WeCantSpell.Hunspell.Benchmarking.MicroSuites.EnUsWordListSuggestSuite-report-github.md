``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-RSDFNM : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.03 ms | 0.477 ms | 0.124 ms |  0.84 |
|             &#39;Suggest root words&#39; | 29.73 ms | 0.554 ms | 0.144 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 30.16 ms | 0.463 ms | 0.120 ms |  1.01 |
|            &#39;Suggest wrong words&#39; | 34.22 ms | 0.584 ms | 0.305 ms |  1.16 |
