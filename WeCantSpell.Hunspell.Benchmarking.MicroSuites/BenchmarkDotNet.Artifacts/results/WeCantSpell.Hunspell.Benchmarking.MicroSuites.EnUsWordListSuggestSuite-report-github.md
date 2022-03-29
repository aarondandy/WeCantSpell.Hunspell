``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-QYTTQF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 22.98 ms | 0.438 ms | 0.317 ms |  0.89 |    0.02 |
|             &#39;Suggest root words&#39; | 25.86 ms | 0.391 ms | 0.139 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 26.76 ms | 0.530 ms | 0.496 ms |  1.05 |    0.02 |
|            &#39;Suggest wrong words&#39; | 29.38 ms | 0.498 ms | 0.260 ms |  1.13 |    0.01 |
