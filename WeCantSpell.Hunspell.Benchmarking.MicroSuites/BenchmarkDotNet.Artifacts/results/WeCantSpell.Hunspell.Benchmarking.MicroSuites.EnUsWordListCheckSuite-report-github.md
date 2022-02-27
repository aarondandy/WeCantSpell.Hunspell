``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-EVCJDP : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,775.1 μs | 163.11 μs | 42.36 μs | 30.77 |    0.18 |
|             &#39;Check root words&#39; |   252.4 μs |   2.58 μs |  0.40 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   769.2 μs |  11.21 μs |  1.74 μs |  3.05 |    0.01 |
|            &#39;Check wrong words&#39; | 6,798.2 μs | 125.32 μs | 55.64 μs | 26.95 |    0.29 |
