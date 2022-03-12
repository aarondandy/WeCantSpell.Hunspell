``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-DFUDWF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,625.8 μs | 157.16 μs |  82.20 μs | 28.76 |    0.93 |
|             &#39;Check root words&#39; |   295.9 μs |   7.51 μs |   8.04 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   862.8 μs |   7.96 μs |   3.53 μs |  2.86 |    0.07 |
|            &#39;Check wrong words&#39; | 7,578.7 μs | 143.39 μs | 103.68 μs | 25.46 |    0.65 |
