``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FPTAEY : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 21.26 ms | 0.371 ms | 0.165 ms |  0.85 |    0.02 |
|             &#39;Suggest root words&#39; | 24.88 ms | 0.466 ms | 0.413 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 24.73 ms | 0.490 ms | 0.355 ms |  1.00 |    0.02 |
|            &#39;Suggest wrong words&#39; | 28.03 ms | 0.442 ms | 0.158 ms |  1.13 |    0.02 |
