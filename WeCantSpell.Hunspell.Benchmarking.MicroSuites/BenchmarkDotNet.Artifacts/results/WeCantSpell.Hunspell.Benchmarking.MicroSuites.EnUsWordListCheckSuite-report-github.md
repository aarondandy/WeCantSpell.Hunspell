``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-REULJP : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,787.0 μs | 64.40 μs |  9.97 μs | 19.13 |    0.05 |
|             &#39;Check root words&#39; |   354.7 μs |  7.00 μs |  1.08 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   828.0 μs | 11.73 μs |  0.64 μs |  2.34 |    0.01 |
|            &#39;Check wrong words&#39; | 5,937.6 μs | 85.57 μs | 30.51 μs | 16.75 |    0.14 |
