``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ZALUUM : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,875.5 μs | 87.51 μs | 22.73 μs | 18.65 |    0.14 |
|             &#39;Check root words&#39; |   369.1 μs |  6.10 μs |  2.71 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   860.0 μs | 11.34 μs |  2.95 μs |  2.33 |    0.02 |
|            &#39;Check wrong words&#39; | 5,898.9 μs | 93.66 μs | 24.32 μs | 16.00 |    0.11 |
