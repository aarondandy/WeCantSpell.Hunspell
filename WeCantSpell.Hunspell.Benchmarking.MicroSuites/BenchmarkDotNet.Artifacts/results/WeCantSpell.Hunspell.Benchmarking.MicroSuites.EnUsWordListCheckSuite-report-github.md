``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-AAOPJL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,230.9 μs | 157.21 μs | 161.44 μs | 27.90 |    0.59 |
|             &#39;Check root words&#39; |   296.1 μs |   4.71 μs |   2.09 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   853.9 μs |  18.87 μs |  20.98 μs |  2.93 |    0.10 |
|            &#39;Check wrong words&#39; | 7,217.6 μs |  97.15 μs |  25.23 μs | 24.37 |    0.28 |
