``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-ZSNNIG : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 26.09 ms | 0.388 ms | 0.101 ms |  0.83 |
|             &#39;Suggest root words&#39; | 31.57 ms | 0.497 ms | 0.129 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 30.44 ms | 0.454 ms | 0.300 ms |  0.97 |
|            &#39;Suggest wrong words&#39; | 33.63 ms | 0.480 ms | 0.171 ms |  1.07 |
