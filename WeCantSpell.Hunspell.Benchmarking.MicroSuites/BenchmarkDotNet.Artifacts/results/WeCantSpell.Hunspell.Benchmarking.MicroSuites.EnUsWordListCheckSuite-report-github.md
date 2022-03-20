``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-PDYCEA : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,116.2 μs | 153.88 μs | 120.14 μs | 25.76 |    0.43 |
|             &#39;Check root words&#39; |   313.7 μs |   5.47 μs |   1.95 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   842.8 μs |  11.67 μs |   3.03 μs |  2.68 |    0.02 |
|            &#39;Check wrong words&#39; | 7,111.2 μs | 104.20 μs |  27.06 μs | 22.64 |    0.14 |
