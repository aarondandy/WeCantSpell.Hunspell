``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-NUBGWY : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,884.8 μs |  72.76 μs | 18.89 μs | 18.21 |    0.13 |
|             &#39;Check root words&#39; |   377.8 μs |   4.70 μs |  1.68 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   859.2 μs |  13.20 μs |  2.04 μs |  2.28 |    0.01 |
|            &#39;Check wrong words&#39; | 6,016.1 μs | 106.54 μs | 16.49 μs | 15.93 |    0.08 |
