``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FIZUWB : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,167.3 μs | 137.04 μs | 81.55 μs | 24.46 |    0.20 |
|             &#39;Check root words&#39; |   332.3 μs |   5.27 μs |  0.29 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   884.7 μs |  20.51 μs |  3.17 μs |  2.66 |    0.01 |
|            &#39;Check wrong words&#39; | 7,183.3 μs |  70.32 μs |  3.85 μs | 21.62 |    0.03 |
