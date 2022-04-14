``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ZPLBES : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,125.0 μs | 90.79 μs | 23.58 μs | 17.81 |    0.12 |
|             &#39;Check root words&#39; |   400.1 μs |  7.42 μs |  1.93 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   893.3 μs | 10.86 μs |  1.68 μs |  2.23 |    0.01 |
|            &#39;Check wrong words&#39; | 6,093.5 μs | 77.38 μs | 11.98 μs | 15.21 |    0.05 |
