``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-CLEGHY : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.75 ms | 0.235 ms | 0.036 ms |  0.86 |
|             &#39;Suggest root words&#39; | 29.90 ms | 0.429 ms | 0.066 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.42 ms | 0.554 ms | 0.144 ms |  0.98 |
|            &#39;Suggest wrong words&#39; | 33.66 ms | 0.606 ms | 0.157 ms |  1.13 |
