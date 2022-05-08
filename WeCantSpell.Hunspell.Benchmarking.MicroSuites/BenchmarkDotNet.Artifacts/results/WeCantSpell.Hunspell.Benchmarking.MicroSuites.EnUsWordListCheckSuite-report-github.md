``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FWMKUK : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,501.7 μs | 67.65 μs | 24.12 μs | 20.24 |    0.10 |
|             &#39;Check root words&#39; |   321.2 μs |  5.01 μs |  1.79 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   740.0 μs | 11.32 μs |  2.94 μs |  2.31 |    0.02 |
|            &#39;Check wrong words&#39; | 5,641.9 μs | 67.37 μs | 10.43 μs | 17.56 |    0.08 |
