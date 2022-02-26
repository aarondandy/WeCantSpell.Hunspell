``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-ULXNTQ : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,829.0 μs | 155.27 μs | 145.24 μs | 29.38 |    0.80 |
|             &#39;Check root words&#39; |   269.2 μs |   2.44 μs |   0.13 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   786.4 μs |  15.03 μs |   9.94 μs |  2.93 |    0.05 |
|            &#39;Check wrong words&#39; | 6,766.1 μs |  87.29 μs |  22.67 μs | 25.10 |    0.09 |
