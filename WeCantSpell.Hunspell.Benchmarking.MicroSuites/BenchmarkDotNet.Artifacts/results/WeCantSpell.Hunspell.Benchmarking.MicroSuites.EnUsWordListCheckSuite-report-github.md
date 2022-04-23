``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ANGCFT : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,644.1 μs | 84.98 μs | 22.07 μs | 19.03 |    0.08 |
|             &#39;Check root words&#39; |   349.5 μs |  4.60 μs |  0.25 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   805.7 μs | 10.24 μs |  2.66 μs |  2.30 |    0.01 |
|            &#39;Check wrong words&#39; | 5,850.9 μs | 87.74 μs | 22.79 μs | 16.74 |    0.09 |
