``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-HJILDU : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |  StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|--------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,060.0 μs | 66.93 μs | 3.67 μs | 17.65 |    0.06 |
|             &#39;Check root words&#39; |   399.6 μs |  7.84 μs | 1.21 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   894.6 μs | 10.78 μs | 0.59 μs |  2.24 |    0.01 |
|            &#39;Check wrong words&#39; | 6,030.8 μs | 52.19 μs | 2.86 μs | 15.08 |    0.04 |
