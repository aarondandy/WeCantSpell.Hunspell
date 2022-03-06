``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-MFUYCP : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,038.2 μs | 146.63 μs | 96.98 μs | 30.52 |    0.54 |
|             &#39;Check root words&#39; |   263.6 μs |   3.08 μs |  0.48 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   803.9 μs |  15.31 μs | 12.79 μs |  3.04 |    0.06 |
|            &#39;Check wrong words&#39; | 7,104.8 μs | 125.93 μs | 32.70 μs | 26.96 |    0.18 |
