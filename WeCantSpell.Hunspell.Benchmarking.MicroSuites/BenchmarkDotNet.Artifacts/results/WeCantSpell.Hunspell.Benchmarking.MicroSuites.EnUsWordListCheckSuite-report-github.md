``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-XQNEVX : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,957.6 μs | 110.29 μs | 17.07 μs | 17.57 |    0.10 |
|             &#39;Check root words&#39; |   396.1 μs |   4.93 μs |  1.28 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   890.6 μs |  12.27 μs |  1.90 μs |  2.25 |    0.00 |
|            &#39;Check wrong words&#39; | 5,985.2 μs |  86.69 μs | 22.51 μs | 15.11 |    0.03 |
