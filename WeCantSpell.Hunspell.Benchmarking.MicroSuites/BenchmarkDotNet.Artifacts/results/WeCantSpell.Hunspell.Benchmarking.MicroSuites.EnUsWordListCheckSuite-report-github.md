``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-BVUGPX : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,354.1 μs | 131.45 μs | 20.34 μs | 28.45 |    0.13 |
|             &#39;Check root words&#39; |   258.5 μs |   4.03 μs |  0.62 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   778.8 μs |   9.76 μs |  1.51 μs |  3.01 |    0.01 |
|            &#39;Check wrong words&#39; | 6,546.3 μs | 125.76 μs | 32.66 μs | 25.37 |    0.11 |
