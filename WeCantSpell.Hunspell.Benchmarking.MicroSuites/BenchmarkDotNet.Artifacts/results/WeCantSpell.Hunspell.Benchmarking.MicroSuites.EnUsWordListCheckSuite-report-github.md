``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-GMUPIW : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,275.2 μs | 154.13 μs | 80.61 μs | 25.27 |    0.29 |
|             &#39;Check root words&#39; |   328.3 μs |   4.83 μs |  0.75 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   872.4 μs |   5.55 μs |  0.86 μs |  2.66 |    0.01 |
|            &#39;Check wrong words&#39; | 7,223.4 μs | 105.19 μs | 37.51 μs | 21.98 |    0.14 |
