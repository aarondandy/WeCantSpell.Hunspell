``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-RYQNVF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,550.8 μs | 146.74 μs | 106.10 μs | 20.33 |    0.35 |
|             &#39;Check root words&#39; |   374.3 μs |   7.37 μs |   3.27 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   965.2 μs |  19.77 μs |  21.97 μs |  2.60 |    0.04 |
|            &#39;Check wrong words&#39; | 6,763.7 μs | 135.25 μs | 144.71 μs | 18.17 |    0.49 |
