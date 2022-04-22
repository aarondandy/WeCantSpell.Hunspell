``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-HTITRT : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,917.2 μs | 87.99 μs | 22.85 μs | 18.88 |    0.10 |
|             &#39;Check root words&#39; |   366.3 μs |  6.62 μs |  1.72 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   832.6 μs | 13.93 μs |  4.97 μs |  2.27 |    0.01 |
|            &#39;Check wrong words&#39; | 6,057.0 μs | 76.90 μs |  4.22 μs | 16.50 |    0.07 |
