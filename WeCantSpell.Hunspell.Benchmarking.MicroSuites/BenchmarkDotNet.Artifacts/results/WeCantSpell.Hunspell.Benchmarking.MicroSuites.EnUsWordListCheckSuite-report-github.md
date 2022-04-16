``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-LIFDMP : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,876.3 μs | 106.14 μs | 16.42 μs | 18.86 |    0.07 |
|             &#39;Check root words&#39; |   364.8 μs |   5.04 μs |  1.31 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   849.7 μs |   8.92 μs |  1.38 μs |  2.33 |    0.01 |
|            &#39;Check wrong words&#39; | 5,917.1 μs | 102.31 μs | 15.83 μs | 16.23 |    0.11 |
