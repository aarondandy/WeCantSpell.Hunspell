``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-HENIXT : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,319.3 μs | 97.27 μs | 15.05 μs | 18.24 |    0.13 |
|             &#39;Check root words&#39; |   401.5 μs |  5.57 μs |  1.98 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   912.5 μs | 15.90 μs |  4.13 μs |  2.27 |    0.01 |
|            &#39;Check wrong words&#39; | 6,289.1 μs | 96.23 μs | 14.89 μs | 15.68 |    0.10 |
