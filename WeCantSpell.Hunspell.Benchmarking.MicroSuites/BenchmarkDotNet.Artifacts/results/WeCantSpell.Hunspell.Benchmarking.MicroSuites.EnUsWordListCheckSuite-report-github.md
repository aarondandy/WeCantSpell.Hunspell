``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FJUBKD : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,150.2 μs | 130.32 μs | 20.17 μs | 17.89 |    0.05 |
|             &#39;Check root words&#39; |   399.5 μs |   7.83 μs |  0.43 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   910.4 μs |  13.77 μs |  3.58 μs |  2.28 |    0.01 |
|            &#39;Check wrong words&#39; | 6,299.0 μs |  57.92 μs |  8.96 μs | 15.76 |    0.04 |
