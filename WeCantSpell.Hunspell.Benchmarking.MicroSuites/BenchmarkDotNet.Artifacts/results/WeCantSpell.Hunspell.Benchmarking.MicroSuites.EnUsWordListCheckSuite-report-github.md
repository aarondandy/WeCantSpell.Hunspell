``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-IBEHEZ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |------------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 10,119.8 μs | 371.08 μs | 427.33 μs | 33.71 |    1.36 |
|             &#39;Check root words&#39; |    299.3 μs |   3.55 μs |   2.11 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |    915.8 μs |  15.49 μs |   6.88 μs |  3.06 |    0.03 |
|            &#39;Check wrong words&#39; |  7,441.8 μs | 164.65 μs |  25.48 μs | 24.91 |    0.11 |
