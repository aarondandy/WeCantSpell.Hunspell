``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-NDWQSH : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,856.8 μs | 117.09 μs | 30.41 μs | 29.82 |    0.22 |
|             &#39;Check root words&#39; |   263.6 μs |   6.26 μs |  0.97 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   788.3 μs |  11.76 μs |  4.19 μs |  2.99 |    0.02 |
|            &#39;Check wrong words&#39; | 7,030.5 μs | 137.23 μs | 21.24 μs | 26.68 |    0.15 |
