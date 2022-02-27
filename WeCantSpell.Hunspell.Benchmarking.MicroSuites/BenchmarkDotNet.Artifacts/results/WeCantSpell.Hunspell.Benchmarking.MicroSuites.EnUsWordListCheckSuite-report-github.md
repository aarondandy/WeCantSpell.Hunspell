``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-NJPCPT : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,748.1 μs | 139.36 μs | 36.19 μs | 28.69 |    0.26 |
|             &#39;Check root words&#39; |   270.1 μs |   3.71 μs |  1.32 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   772.4 μs |   6.95 μs |  1.08 μs |  2.85 |    0.01 |
|            &#39;Check wrong words&#39; | 6,765.1 μs | 124.12 μs | 44.26 μs | 25.05 |    0.17 |
