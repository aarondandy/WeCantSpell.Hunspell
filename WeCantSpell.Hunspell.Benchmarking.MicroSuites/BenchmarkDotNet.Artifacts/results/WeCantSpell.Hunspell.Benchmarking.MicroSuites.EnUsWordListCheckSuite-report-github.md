``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-QRHGPC : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,070.8 μs | 159.81 μs | 70.96 μs | 30.48 |    0.35 |
|             &#39;Check root words&#39; |   264.8 μs |   4.17 μs |  1.85 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   791.9 μs |   9.11 μs |  3.25 μs |  2.99 |    0.02 |
|            &#39;Check wrong words&#39; | 6,931.8 μs | 100.23 μs | 15.51 μs | 26.20 |    0.26 |
