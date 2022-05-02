``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-IMBQZN : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,730.8 μs | 73.23 μs | 11.33 μs | 19.49 |    0.06 |
|             &#39;Check root words&#39; |   345.6 μs |  4.58 μs |  1.19 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   787.2 μs | 13.68 μs |  3.55 μs |  2.28 |    0.01 |
|            &#39;Check wrong words&#39; | 5,880.5 μs | 57.99 μs |  8.97 μs | 17.03 |    0.05 |
