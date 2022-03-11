``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-VBVQFN : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,192.3 μs | 160.03 μs | 177.87 μs | 29.58 |    0.79 |
|             &#39;Check root words&#39; |   283.6 μs |   4.85 μs |   0.75 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   818.1 μs |  11.72 μs |   3.04 μs |  2.89 |    0.02 |
|            &#39;Check wrong words&#39; | 6,970.5 μs | 113.68 μs |  50.48 μs | 24.67 |    0.19 |
