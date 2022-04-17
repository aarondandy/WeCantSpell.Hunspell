``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-UUZVHV : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |    Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|---------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,901.9 μs | 31.76 μs |  1.74 μs | 18.69 |    0.08 |
|             &#39;Check root words&#39; |   369.2 μs |  5.06 μs |  1.31 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   859.2 μs | 15.07 μs |  2.33 μs |  2.33 |    0.01 |
|            &#39;Check wrong words&#39; | 5,874.1 μs | 75.85 μs | 19.70 μs | 15.91 |    0.10 |
