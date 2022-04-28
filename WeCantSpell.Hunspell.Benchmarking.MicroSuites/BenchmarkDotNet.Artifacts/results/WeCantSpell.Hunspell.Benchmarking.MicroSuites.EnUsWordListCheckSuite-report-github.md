``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-OVBNHI : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,604.2 μs | 85.78 μs | 13.27 μs | 18.71 |    0.10 |
|             &#39;Check root words&#39; |   353.7 μs |  5.57 μs |  2.47 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   797.9 μs | 12.80 μs |  1.98 μs |  2.26 |    0.02 |
|            &#39;Check wrong words&#39; | 5,792.3 μs | 64.15 μs |  9.93 μs | 16.41 |    0.13 |
