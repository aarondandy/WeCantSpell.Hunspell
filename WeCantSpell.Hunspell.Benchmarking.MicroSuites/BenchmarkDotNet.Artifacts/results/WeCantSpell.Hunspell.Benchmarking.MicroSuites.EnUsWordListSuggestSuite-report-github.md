``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-YRKMOI : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 27.30 ms | 1.064 ms | 1.225 ms |  0.89 |    0.05 |
|             &#39;Suggest root words&#39; | 30.61 ms | 0.990 ms | 1.140 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 34.29 ms | 0.799 ms | 0.920 ms |  1.12 |    0.05 |
|            &#39;Suggest wrong words&#39; | 36.74 ms | 2.417 ms | 2.783 ms |  1.20 |    0.11 |
