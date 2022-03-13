``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-QSOGKR : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,237.2 μs | 148.16 μs | 52.83 μs | 27.71 |    0.19 |
|             &#39;Check root words&#39; |   297.1 μs |   5.52 μs |  0.85 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   821.7 μs |  11.40 μs |  2.96 μs |  2.77 |    0.01 |
|            &#39;Check wrong words&#39; | 7,253.7 μs | 100.40 μs | 26.07 μs | 24.40 |    0.09 |
