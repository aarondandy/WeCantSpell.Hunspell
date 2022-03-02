``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-NACPDG : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 25.70 ms | 0.328 ms | 0.117 ms |  0.83 |    0.01 |
|             &#39;Suggest root words&#39; | 30.86 ms | 0.547 ms | 0.195 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 30.15 ms | 0.459 ms | 0.119 ms |  0.98 |    0.01 |
|            &#39;Suggest wrong words&#39; | 34.69 ms | 0.631 ms | 0.375 ms |  1.13 |    0.02 |
