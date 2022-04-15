``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ISCIAJ : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,975.8 μs | 121.28 μs | 31.50 μs | 17.64 |    0.14 |
|             &#39;Check root words&#39; |   395.7 μs |   7.81 μs |  1.21 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   897.9 μs |  17.85 μs |  6.37 μs |  2.27 |    0.01 |
|            &#39;Check wrong words&#39; | 6,007.6 μs |  99.50 μs | 44.18 μs | 15.17 |    0.16 |
