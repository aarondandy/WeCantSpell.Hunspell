``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-UETZQQ : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,704.3 μs | 121.84 μs | 18.86 μs | 29.81 |    0.05 |
|             &#39;Check root words&#39; |   258.3 μs |   3.43 μs |  0.89 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   788.8 μs |  15.40 μs |  4.00 μs |  3.05 |    0.01 |
|            &#39;Check wrong words&#39; | 7,005.2 μs | 109.14 μs | 28.34 μs | 27.12 |    0.14 |
