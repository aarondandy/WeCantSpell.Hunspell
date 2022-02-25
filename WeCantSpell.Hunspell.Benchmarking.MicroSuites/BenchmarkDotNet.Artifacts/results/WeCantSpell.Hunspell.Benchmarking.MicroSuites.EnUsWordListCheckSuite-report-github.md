``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-WGFYVD : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,518.6 μs | 121.30 μs |  63.44 μs | 29.35 |    0.33 |
|             &#39;Check root words&#39; |   257.2 μs |   4.08 μs |   1.06 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   775.9 μs |   7.62 μs |   0.42 μs |  3.01 |    0.00 |
|            &#39;Check wrong words&#39; | 6,722.0 μs | 132.09 μs | 123.56 μs | 26.21 |    0.27 |
