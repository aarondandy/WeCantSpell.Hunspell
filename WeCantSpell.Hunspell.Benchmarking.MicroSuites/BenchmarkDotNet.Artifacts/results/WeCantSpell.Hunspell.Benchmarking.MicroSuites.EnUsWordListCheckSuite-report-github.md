``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-RSDFNM : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,967.0 μs | 240.58 μs | 277.05 μs | 27.90 |    1.20 |
|             &#39;Check root words&#39; |   286.2 μs |   5.66 μs |   6.06 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   817.6 μs |  15.61 μs |   8.16 μs |  2.89 |    0.07 |
|            &#39;Check wrong words&#39; | 7,107.0 μs | 255.48 μs | 294.21 μs | 24.94 |    1.37 |
