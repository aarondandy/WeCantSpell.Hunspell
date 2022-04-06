``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-WYMXMR : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,178.8 μs | 102.05 μs | 26.50 μs | 17.78 |    0.09 |
|             &#39;Check root words&#39; |   403.7 μs |   8.07 μs |  2.10 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   895.6 μs |  15.29 μs |  3.97 μs |  2.22 |    0.02 |
|            &#39;Check wrong words&#39; | 6,128.5 μs |  79.13 μs | 12.25 μs | 15.19 |    0.07 |
