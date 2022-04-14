``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-MOMCBI : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,126.3 μs | 78.19 μs | 20.31 μs | 17.59 |    0.04 |
|             &#39;Check root words&#39; |   405.2 μs |  5.50 μs |  1.43 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   912.1 μs | 10.31 μs |  2.68 μs |  2.25 |    0.01 |
|            &#39;Check wrong words&#39; | 6,139.8 μs | 79.77 μs | 20.72 μs | 15.15 |    0.10 |
