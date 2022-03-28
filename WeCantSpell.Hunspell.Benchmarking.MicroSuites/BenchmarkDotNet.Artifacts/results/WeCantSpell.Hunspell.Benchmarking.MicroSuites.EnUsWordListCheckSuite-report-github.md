``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-JWILFL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,202.6 μs | 110.01 μs | 28.57 μs | 21.78 |    0.17 |
|             &#39;Check root words&#39; |   330.6 μs |   6.35 μs |  2.26 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   863.3 μs |  11.67 μs |  1.81 μs |  2.61 |    0.02 |
|            &#39;Check wrong words&#39; | 6,630.4 μs |  54.18 μs |  2.97 μs | 20.09 |    0.19 |
