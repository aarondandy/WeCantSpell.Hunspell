``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-PIKFAO : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,994.6 μs | 72.35 μs | 11.20 μs | 19.23 |    0.04 |
|             &#39;Check root words&#39; |   363.8 μs |  4.66 μs |  0.72 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   842.7 μs | 15.31 μs |  3.98 μs |  2.32 |    0.01 |
|            &#39;Check wrong words&#39; | 5,957.7 μs | 79.69 μs | 12.33 μs | 16.38 |    0.04 |
