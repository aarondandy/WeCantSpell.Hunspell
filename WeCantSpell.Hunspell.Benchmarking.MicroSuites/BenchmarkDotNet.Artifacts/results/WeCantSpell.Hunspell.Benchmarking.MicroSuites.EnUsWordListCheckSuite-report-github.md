``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-YRKMOI : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,156.4 μs | 107.50 μs | 16.64 μs | 19.50 |    0.08 |
|             &#39;Check root words&#39; |   418.4 μs |   5.23 μs |  0.81 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   946.1 μs |   5.20 μs |  0.28 μs |  2.26 |    0.01 |
|            &#39;Check wrong words&#39; | 6,557.8 μs | 110.32 μs | 48.98 μs | 15.66 |    0.13 |
