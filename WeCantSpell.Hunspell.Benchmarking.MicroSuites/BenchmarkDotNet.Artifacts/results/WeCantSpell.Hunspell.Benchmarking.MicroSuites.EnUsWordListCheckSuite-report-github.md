``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TPTUPA : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,281.2 μs | 113.95 μs | 40.64 μs | 17.89 |    0.15 |
|             &#39;Check root words&#39; |   407.0 μs |   4.65 μs |  1.21 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   912.6 μs |  13.01 μs |  2.01 μs |  2.24 |    0.01 |
|            &#39;Check wrong words&#39; | 6,257.7 μs |  83.52 μs | 12.93 μs | 15.38 |    0.04 |
