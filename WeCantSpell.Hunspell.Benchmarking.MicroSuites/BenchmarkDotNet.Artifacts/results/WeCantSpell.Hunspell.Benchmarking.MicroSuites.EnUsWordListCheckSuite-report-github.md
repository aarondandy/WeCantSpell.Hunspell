``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FKUIZY : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,792.9 μs | 114.76 μs | 17.76 μs | 18.11 |    0.05 |
|             &#39;Check root words&#39; |   374.9 μs |   5.82 μs |  1.51 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   849.9 μs |  15.72 μs |  4.08 μs |  2.27 |    0.01 |
|            &#39;Check wrong words&#39; | 5,825.1 μs |  79.93 μs | 20.76 μs | 15.54 |    0.04 |
