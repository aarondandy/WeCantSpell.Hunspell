``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-LZSJPM : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,165.2 μs | 131.39 μs | 102.58 μs | 24.04 |    0.20 |
|             &#39;Check root words&#39; |   335.9 μs |   5.02 μs |   0.28 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   875.4 μs |  14.29 μs |   2.21 μs |  2.60 |    0.00 |
|            &#39;Check wrong words&#39; | 7,113.2 μs |  94.49 μs |  24.54 μs | 21.19 |    0.11 |
