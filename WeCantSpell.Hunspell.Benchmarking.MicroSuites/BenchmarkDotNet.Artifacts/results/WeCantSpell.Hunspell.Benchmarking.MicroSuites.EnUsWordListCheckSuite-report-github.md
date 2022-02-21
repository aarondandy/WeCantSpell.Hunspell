``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-ATTJGT : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,128.1 μs | 112.88 μs | 29.31 μs | 29.07 |    0.50 |
|             &#39;Check root words&#39; |   244.6 μs |   3.65 μs |  3.05 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   732.7 μs |  13.06 μs |  3.39 μs |  2.99 |    0.05 |
|            &#39;Check wrong words&#39; | 6,312.1 μs |  76.90 μs | 19.97 μs | 25.74 |    0.52 |
