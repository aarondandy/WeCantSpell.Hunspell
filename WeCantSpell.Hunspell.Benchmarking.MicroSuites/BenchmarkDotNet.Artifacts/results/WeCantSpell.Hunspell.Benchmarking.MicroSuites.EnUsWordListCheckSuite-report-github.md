``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-KEZYEW : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,251.1 μs |  92.21 μs | 14.27 μs | 21.56 |    0.06 |
|             &#39;Check root words&#39; |   336.3 μs |   4.37 μs |  0.68 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   885.8 μs |  15.87 μs |  5.66 μs |  2.64 |    0.02 |
|            &#39;Check wrong words&#39; | 6,312.4 μs | 101.51 μs | 26.36 μs | 18.79 |    0.06 |
