``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-AUEQJS : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,739.7 μs | 80.06 μs | 12.39 μs | 19.52 |    0.08 |
|             &#39;Check root words&#39; |   345.5 μs |  6.45 μs |  1.67 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   786.2 μs | 13.57 μs |  7.10 μs |  2.28 |    0.02 |
|            &#39;Check wrong words&#39; | 5,743.4 μs | 73.80 μs | 11.42 μs | 16.64 |    0.10 |
