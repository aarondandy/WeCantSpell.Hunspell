``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-BIZISB : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,333.2 μs |  85.10 μs | 13.17 μs | 21.98 |    0.03 |
|             &#39;Check root words&#39; |   333.6 μs |   6.05 μs |  0.94 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   890.2 μs |   5.48 μs |  0.85 μs |  2.67 |    0.01 |
|            &#39;Check wrong words&#39; | 6,269.8 μs | 115.82 μs | 41.30 μs | 18.79 |    0.13 |
