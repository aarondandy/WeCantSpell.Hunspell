``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FRGEQI : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,406.7 μs | 69.70 μs | 10.79 μs | 19.40 |    0.05 |
|             &#39;Check root words&#39; |   330.2 μs |  6.25 μs |  0.97 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   740.3 μs | 13.95 μs |  9.23 μs |  2.25 |    0.03 |
|            &#39;Check wrong words&#39; | 5,622.8 μs | 57.64 μs | 14.97 μs | 17.03 |    0.05 |
