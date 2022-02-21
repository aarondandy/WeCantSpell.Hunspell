``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-SQFNHB : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,478.7 μs | 149.24 μs | 98.72 μs | 29.24 |    0.47 |
|             &#39;Check root words&#39; |   256.2 μs |   4.39 μs |  1.95 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   762.3 μs |  15.05 μs |  6.68 μs |  2.98 |    0.01 |
|            &#39;Check wrong words&#39; | 6,567.2 μs | 118.35 μs | 18.31 μs | 25.67 |    0.17 |
