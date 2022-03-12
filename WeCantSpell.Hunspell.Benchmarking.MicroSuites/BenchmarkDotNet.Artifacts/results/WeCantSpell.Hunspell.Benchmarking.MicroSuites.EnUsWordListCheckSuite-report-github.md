``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FOVRWU : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,694.8 μs | 166.66 μs | 74.00 μs | 30.37 |    0.40 |
|             &#39;Check root words&#39; |   285.5 μs |   5.57 μs |  3.69 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   850.4 μs |  16.56 μs | 14.68 μs |  2.99 |    0.08 |
|            &#39;Check wrong words&#39; | 7,613.8 μs | 141.93 μs | 74.23 μs | 26.64 |    0.38 |
