``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-PXVEID : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,062.8 μs | 160.60 μs | 134.11 μs | 28.89 |    0.42 |
|             &#39;Check root words&#39; |   280.4 μs |   5.40 μs |   1.40 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   809.8 μs |  10.29 μs |   2.67 μs |  2.89 |    0.02 |
|            &#39;Check wrong words&#39; | 7,125.7 μs | 123.11 μs |  64.39 μs | 25.39 |    0.22 |
