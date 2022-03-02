``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-NACPDG : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,064.1 μs | 136.68 μs | 60.69 μs | 29.86 |    0.16 |
|             &#39;Check root words&#39; |   269.7 μs |   3.19 μs |  0.83 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   782.6 μs |  13.35 μs |  2.07 μs |  2.90 |    0.02 |
|            &#39;Check wrong words&#39; | 7,010.6 μs |  88.83 μs | 23.07 μs | 26.00 |    0.05 |
