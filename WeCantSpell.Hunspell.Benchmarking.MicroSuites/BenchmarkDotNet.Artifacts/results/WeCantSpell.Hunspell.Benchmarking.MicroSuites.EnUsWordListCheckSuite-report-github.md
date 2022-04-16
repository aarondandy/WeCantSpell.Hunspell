``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-UKDIFP : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,803.0 μs | 123.90 μs | 19.17 μs | 18.76 |    0.05 |
|             &#39;Check root words&#39; |   362.6 μs |   5.68 μs |  0.88 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   855.9 μs |  12.45 μs |  1.93 μs |  2.36 |    0.01 |
|            &#39;Check wrong words&#39; | 5,878.5 μs | 106.70 μs | 27.71 μs | 16.20 |    0.12 |
