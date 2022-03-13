``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FPTAEY : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,521.5 μs | 236.82 μs | 272.73 μs | 29.21 |    0.69 |
|             &#39;Check root words&#39; |   286.6 μs |   5.46 μs |   1.95 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   847.9 μs |  16.41 μs |  17.56 μs |  2.95 |    0.08 |
|            &#39;Check wrong words&#39; | 7,458.2 μs | 145.53 μs | 142.93 μs | 26.00 |    0.74 |
