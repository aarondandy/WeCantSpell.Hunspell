``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-LDAGOI : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,321.0 μs | 150.70 μs | 89.68 μs | 29.43 |    0.31 |
|             &#39;Check root words&#39; |   284.5 μs |   4.91 μs |  1.27 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   835.2 μs |  12.56 μs |  1.94 μs |  2.94 |    0.02 |
|            &#39;Check wrong words&#39; | 7,343.0 μs |  97.74 μs | 25.38 μs | 25.81 |    0.17 |
