``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-BYZJYM : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,942.1 μs | 143.74 μs | 37.33 μs | 29.76 |    0.15 |
|             &#39;Check root words&#39; |   267.0 μs |   3.30 μs |  0.51 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   813.9 μs |  12.70 μs |  1.97 μs |  3.05 |    0.01 |
|            &#39;Check wrong words&#39; | 6,792.7 μs | 101.94 μs | 36.35 μs | 25.37 |    0.11 |
