``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ZHZTQI : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,928.3 μs |  56.12 μs |  8.68 μs | 18.07 |    0.10 |
|             &#39;Check root words&#39; |   383.6 μs |   5.85 μs |  2.09 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   861.3 μs |   5.98 μs |  0.93 μs |  2.25 |    0.02 |
|            &#39;Check wrong words&#39; | 5,920.1 μs | 102.20 μs | 26.54 μs | 15.42 |    0.12 |
