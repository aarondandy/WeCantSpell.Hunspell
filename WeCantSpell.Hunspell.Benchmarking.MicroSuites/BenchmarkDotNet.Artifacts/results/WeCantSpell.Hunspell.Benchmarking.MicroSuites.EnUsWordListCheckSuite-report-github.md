``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ESYPON : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,211.8 μs | 71.43 μs | 18.55 μs | 17.91 |    0.05 |
|             &#39;Check root words&#39; |   402.7 μs |  5.72 μs |  1.48 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   899.4 μs | 15.08 μs |  2.33 μs |  2.23 |    0.01 |
|            &#39;Check wrong words&#39; | 6,205.0 μs | 81.61 μs | 21.19 μs | 15.41 |    0.05 |
