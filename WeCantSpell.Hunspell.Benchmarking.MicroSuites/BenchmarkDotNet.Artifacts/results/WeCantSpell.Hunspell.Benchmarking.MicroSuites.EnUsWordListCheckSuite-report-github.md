``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-HBFRGN : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,760.4 μs | 125.23 μs |  32.52 μs | 27.67 |    0.27 |
|             &#39;Check root words&#39; |   280.6 μs |   4.97 μs |   1.77 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   814.1 μs |  14.31 μs |   2.21 μs |  2.90 |    0.02 |
|            &#39;Check wrong words&#39; | 7,021.6 μs | 137.73 μs | 128.83 μs | 25.09 |    0.71 |
