``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-NWEBMO : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,728.3 μs | 90.87 μs |  4.98 μs | 19.19 |    0.07 |
|             &#39;Check root words&#39; |   351.2 μs |  4.42 μs |  1.15 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   796.5 μs | 12.31 μs |  5.46 μs |  2.27 |    0.02 |
|            &#39;Check wrong words&#39; | 5,746.4 μs | 67.02 μs | 10.37 μs | 16.37 |    0.05 |
