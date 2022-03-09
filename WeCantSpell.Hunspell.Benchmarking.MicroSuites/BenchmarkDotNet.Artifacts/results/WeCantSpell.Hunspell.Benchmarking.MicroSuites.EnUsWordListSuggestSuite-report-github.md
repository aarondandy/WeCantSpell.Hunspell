``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-AAOPJL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 25.96 ms | 0.484 ms | 0.405 ms |  0.85 |    0.02 |
|             &#39;Suggest root words&#39; | 30.50 ms | 0.410 ms | 0.215 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 30.86 ms | 0.253 ms | 0.039 ms |  1.01 |    0.01 |
|            &#39;Suggest wrong words&#39; | 33.75 ms | 0.458 ms | 0.025 ms |  1.10 |    0.01 |
