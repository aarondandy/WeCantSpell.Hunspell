``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FBWXJG : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,326.9 μs | 98.93 μs | 25.69 μs | 19.15 |    0.14 |
|             &#39;Check root words&#39; |   330.3 μs |  4.15 μs |  1.08 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   736.1 μs | 11.11 μs |  1.72 μs |  2.23 |    0.01 |
|            &#39;Check wrong words&#39; | 5,500.0 μs | 66.85 μs | 10.35 μs | 16.65 |    0.09 |
