``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-MIJBKN : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |------------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 10,271.4 μs | 263.71 μs | 282.17 μs | 32.69 |    1.39 |
|             &#39;Check root words&#39; |    306.9 μs |   4.67 μs |   1.21 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |    923.2 μs |  14.23 μs |   6.32 μs |  3.00 |    0.02 |
|            &#39;Check wrong words&#39; |  8,767.7 μs | 165.13 μs | 162.18 μs | 28.72 |    0.55 |
