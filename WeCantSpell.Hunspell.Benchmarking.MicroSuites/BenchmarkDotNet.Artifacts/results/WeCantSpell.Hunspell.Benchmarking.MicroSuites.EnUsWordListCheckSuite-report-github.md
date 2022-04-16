``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-JMEKGW : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,934.6 μs | 132.63 μs | 34.44 μs | 18.56 |    0.14 |
|             &#39;Check root words&#39; |   373.6 μs |   5.87 μs |  1.52 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   848.5 μs |  15.02 μs |  2.32 μs |  2.27 |    0.01 |
|            &#39;Check wrong words&#39; | 5,963.7 μs |  93.20 μs |  5.11 μs | 15.93 |    0.07 |
