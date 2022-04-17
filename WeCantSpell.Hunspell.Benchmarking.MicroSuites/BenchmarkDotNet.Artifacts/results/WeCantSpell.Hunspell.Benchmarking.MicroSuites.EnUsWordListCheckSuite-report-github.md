``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-XJPVAV : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,897.9 μs | 83.85 μs | 12.98 μs | 18.80 |    0.14 |
|             &#39;Check root words&#39; |   366.5 μs |  6.26 μs |  2.23 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   855.8 μs | 16.78 μs |  5.98 μs |  2.34 |    0.01 |
|            &#39;Check wrong words&#39; | 5,892.4 μs | 90.74 μs | 14.04 μs | 16.06 |    0.09 |
