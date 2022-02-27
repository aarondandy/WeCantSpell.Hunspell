``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-JNVTFH : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,625.0 μs | 121.41 μs | 31.53 μs | 30.04 |    0.16 |
|             &#39;Check root words&#39; |   253.9 μs |   4.74 μs |  1.69 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   778.3 μs |  12.25 μs |  3.18 μs |  3.07 |    0.02 |
|            &#39;Check wrong words&#39; | 6,818.2 μs | 132.82 μs | 47.36 μs | 26.86 |    0.19 |
