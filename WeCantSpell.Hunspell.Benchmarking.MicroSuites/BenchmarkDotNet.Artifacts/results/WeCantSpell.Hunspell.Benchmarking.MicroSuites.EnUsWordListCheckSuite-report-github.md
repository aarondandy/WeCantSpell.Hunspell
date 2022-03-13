``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-VYVIDP : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,555.7 μs | 162.34 μs | 159.44 μs | 25.63 |    0.83 |
|             &#39;Check root words&#39; |   333.3 μs |   6.45 μs |   6.90 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   911.5 μs |  17.25 μs |  12.47 μs |  2.72 |    0.07 |
|            &#39;Check wrong words&#39; | 7,377.2 μs | 142.82 μs | 158.75 μs | 22.13 |    0.73 |
