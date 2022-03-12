``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-ABOOUB : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 26.87 ms | 0.345 ms | 0.228 ms |  0.89 |    0.01 |
|             &#39;Suggest root words&#39; | 30.40 ms | 0.453 ms | 0.118 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 29.99 ms | 0.521 ms | 0.135 ms |  0.99 |    0.01 |
|            &#39;Suggest wrong words&#39; | 33.58 ms | 0.460 ms | 0.332 ms |  1.11 |    0.02 |
