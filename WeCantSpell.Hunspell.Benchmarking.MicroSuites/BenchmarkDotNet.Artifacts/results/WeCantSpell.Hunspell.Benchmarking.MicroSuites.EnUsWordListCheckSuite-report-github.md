``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-BSLIAV : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,656.5 μs | 81.62 μs | 12.63 μs | 19.54 |    0.02 |
|             &#39;Check root words&#39; |   340.7 μs |  5.87 μs |  0.91 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   798.1 μs | 12.09 μs |  1.87 μs |  2.34 |    0.01 |
|            &#39;Check wrong words&#39; | 5,855.8 μs | 95.12 μs | 14.72 μs | 17.19 |    0.04 |
