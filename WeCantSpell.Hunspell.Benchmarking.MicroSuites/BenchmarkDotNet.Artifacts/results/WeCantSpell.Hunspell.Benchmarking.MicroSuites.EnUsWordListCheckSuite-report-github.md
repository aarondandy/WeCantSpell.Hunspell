``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TTTCWL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,028.9 μs | 194.46 μs | 208.07 μs | 29.08 |    0.75 |
|             &#39;Check root words&#39; |   275.6 μs |   5.10 μs |   2.27 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   793.8 μs |  13.85 μs |   4.94 μs |  2.88 |    0.03 |
|            &#39;Check wrong words&#39; | 6,668.6 μs | 132.48 μs |  20.50 μs | 24.23 |    0.29 |
