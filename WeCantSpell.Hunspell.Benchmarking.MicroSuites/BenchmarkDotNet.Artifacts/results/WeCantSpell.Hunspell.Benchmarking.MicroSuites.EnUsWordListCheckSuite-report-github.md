``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-EHFOEA : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,832.7 μs | 150.46 μs | 53.66 μs | 30.19 |    0.27 |
|             &#39;Check root words&#39; |   259.9 μs |   4.46 μs |  1.16 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   791.8 μs |  12.74 μs |  5.66 μs |  3.05 |    0.02 |
|            &#39;Check wrong words&#39; | 6,926.3 μs | 114.23 μs | 50.72 μs | 26.62 |    0.31 |
