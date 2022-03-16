``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-UZIGPF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,283.4 μs | 156.79 μs | 55.91 μs | 25.38 |    0.28 |
|             &#39;Check root words&#39; |   326.4 μs |   6.01 μs |  2.14 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   881.0 μs |   9.72 μs |  1.50 μs |  2.71 |    0.01 |
|            &#39;Check wrong words&#39; | 7,324.1 μs | 100.93 μs | 15.62 μs | 22.52 |    0.07 |
