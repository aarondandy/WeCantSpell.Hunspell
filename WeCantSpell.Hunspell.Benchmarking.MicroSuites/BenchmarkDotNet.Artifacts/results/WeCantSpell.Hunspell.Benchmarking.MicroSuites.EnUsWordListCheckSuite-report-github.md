``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-RMGOJL : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,203.8 μs | 240.90 μs | 257.76 μs | 29.23 |    0.98 |
|             &#39;Check root words&#39; |   284.2 μs |   4.88 μs |   2.91 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   829.1 μs |  14.86 μs |   5.30 μs |  2.91 |    0.05 |
|            &#39;Check wrong words&#39; | 6,762.7 μs | 117.44 μs |  30.50 μs | 23.69 |    0.34 |
