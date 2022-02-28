``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-YIIEFG : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,794.4 μs | 117.18 μs | 91.49 μs | 28.90 |    0.47 |
|             &#39;Check root words&#39; |   270.1 μs |   4.82 μs |  2.14 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   785.0 μs |  13.00 μs |  5.77 μs |  2.91 |    0.03 |
|            &#39;Check wrong words&#39; | 6,906.7 μs | 120.78 μs | 63.17 μs | 25.57 |    0.32 |
