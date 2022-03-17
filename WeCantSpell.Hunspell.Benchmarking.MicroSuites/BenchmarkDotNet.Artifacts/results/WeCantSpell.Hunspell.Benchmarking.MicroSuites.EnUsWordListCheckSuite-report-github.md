``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-SBBIOJ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,090.6 μs | 155.75 μs | 69.16 μs | 24.70 |    0.20 |
|             &#39;Check root words&#39; |   327.1 μs |   4.47 μs |  2.66 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   881.4 μs |  14.31 μs |  3.72 μs |  2.68 |    0.02 |
|            &#39;Check wrong words&#39; | 7,166.2 μs | 126.22 μs | 45.01 μs | 21.86 |    0.22 |
