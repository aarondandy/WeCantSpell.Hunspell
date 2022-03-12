``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-ABOOUB : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,395.2 μs | 153.69 μs | 128.33 μs | 27.57 |    1.20 |
|             &#39;Check root words&#39; |   297.8 μs |  12.54 μs |  14.44 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   858.3 μs |  18.07 μs |  20.08 μs |  2.88 |    0.16 |
|            &#39;Check wrong words&#39; | 7,359.0 μs | 143.25 μs | 147.10 μs | 24.60 |    1.40 |
