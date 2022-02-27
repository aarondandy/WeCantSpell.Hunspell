``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-NJPCPT : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 24.93 ms | 0.431 ms | 0.154 ms |  0.83 |
|             &#39;Suggest root words&#39; | 29.99 ms | 0.588 ms | 0.153 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.81 ms | 0.576 ms | 0.301 ms |  1.00 |
|            &#39;Suggest wrong words&#39; | 33.01 ms | 0.497 ms | 0.129 ms |  1.10 |
