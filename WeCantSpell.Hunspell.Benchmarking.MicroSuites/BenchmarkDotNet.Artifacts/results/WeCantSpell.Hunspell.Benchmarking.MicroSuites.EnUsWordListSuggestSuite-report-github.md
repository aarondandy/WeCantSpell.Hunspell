``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-JMEKGW : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.97 ms | 0.401 ms | 0.239 ms |  0.91 |
|             &#39;Suggest root words&#39; | 24.04 ms | 0.370 ms | 0.096 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.74 ms | 0.423 ms | 0.110 ms |  1.03 |
|            &#39;Suggest wrong words&#39; | 27.71 ms | 0.518 ms | 0.271 ms |  1.15 |
