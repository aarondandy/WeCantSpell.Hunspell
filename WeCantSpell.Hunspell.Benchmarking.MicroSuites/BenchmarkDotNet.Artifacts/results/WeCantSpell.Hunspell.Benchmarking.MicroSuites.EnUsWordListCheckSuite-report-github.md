``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-JGTVJH : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,788.8 μs | 138.48 μs | 35.96 μs | 28.91 |    0.33 |
|             &#39;Check root words&#39; |   269.1 μs |   4.24 μs |  1.88 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   805.0 μs |  15.04 μs |  2.33 μs |  2.99 |    0.03 |
|            &#39;Check wrong words&#39; | 6,797.5 μs |  84.80 μs | 22.02 μs | 25.23 |    0.23 |
