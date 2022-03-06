``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-ADJPVC : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,748.8 μs | 109.94 μs | 57.50 μs | 28.52 |    0.19 |
|             &#39;Check root words&#39; |   271.9 μs |   4.84 μs |  1.73 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   796.3 μs |  13.62 μs |  2.11 μs |  2.93 |    0.03 |
|            &#39;Check wrong words&#39; | 6,913.8 μs | 111.32 μs | 49.43 μs | 25.45 |    0.33 |
