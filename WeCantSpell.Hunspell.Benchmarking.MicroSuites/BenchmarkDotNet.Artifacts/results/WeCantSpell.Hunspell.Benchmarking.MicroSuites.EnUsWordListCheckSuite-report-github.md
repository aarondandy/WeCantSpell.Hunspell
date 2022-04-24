``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-LNJFVA : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |  StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|--------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,756.3 μs | 64.30 μs | 3.52 μs | 19.86 |    0.15 |
|             &#39;Check root words&#39; |   340.7 μs |  6.10 μs | 2.71 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   807.7 μs | 13.92 μs | 3.62 μs |  2.38 |    0.02 |
|            &#39;Check wrong words&#39; | 5,866.8 μs | 49.33 μs | 7.63 μs | 17.24 |    0.08 |
