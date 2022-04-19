``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ATICDS : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,048.1 μs | 100.86 μs | 26.19 μs | 18.68 |    0.13 |
|             &#39;Check root words&#39; |   377.8 μs |   6.43 μs |  2.29 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   868.0 μs |  10.93 μs |  4.85 μs |  2.30 |    0.02 |
|            &#39;Check wrong words&#39; | 6,079.0 μs |  68.20 μs | 10.55 μs | 16.12 |    0.13 |
