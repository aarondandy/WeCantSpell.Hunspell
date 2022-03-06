``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-CLEGHY : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,963.8 μs | 152.92 μs | 169.97 μs | 30.34 |    0.53 |
|             &#39;Check root words&#39; |   263.9 μs |   4.57 μs |   2.39 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   796.5 μs |  12.20 μs |   3.17 μs |  3.02 |    0.02 |
|            &#39;Check wrong words&#39; | 6,873.9 μs | 120.86 μs |  31.39 μs | 26.10 |    0.28 |
