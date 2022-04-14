``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-OVLAKM : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,227.9 μs | 125.44 μs | 32.58 μs | 18.03 |    0.09 |
|             &#39;Check root words&#39; |   401.5 μs |   6.05 μs |  0.33 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   893.3 μs |  15.45 μs |  2.39 μs |  2.23 |    0.00 |
|            &#39;Check wrong words&#39; | 6,157.6 μs |  96.04 μs | 34.25 μs | 15.34 |    0.12 |
