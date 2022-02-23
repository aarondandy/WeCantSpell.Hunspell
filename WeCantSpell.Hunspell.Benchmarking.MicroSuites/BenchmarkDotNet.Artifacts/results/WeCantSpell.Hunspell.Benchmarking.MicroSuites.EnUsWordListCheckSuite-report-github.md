``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-JWFWCJ : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,548.4 μs | 114.12 μs | 29.64 μs | 29.44 |    0.22 |
|             &#39;Check root words&#39; |   256.0 μs |   4.42 μs |  1.57 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   778.4 μs |  11.19 μs |  1.73 μs |  3.04 |    0.02 |
|            &#39;Check wrong words&#39; | 6,717.7 μs | 116.60 μs | 51.77 μs | 26.24 |    0.30 |
