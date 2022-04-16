``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-RXLWXU : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,884.7 μs | 111.47 μs | 17.25 μs | 17.05 |    0.15 |
|             &#39;Check root words&#39; |   402.0 μs |   6.16 μs |  3.22 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   887.2 μs |  17.47 μs |  2.70 μs |  2.20 |    0.02 |
|            &#39;Check wrong words&#39; | 6,015.5 μs | 115.33 μs | 51.21 μs | 14.95 |    0.17 |
