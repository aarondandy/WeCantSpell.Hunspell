``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TEWBAJ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,952.0 μs |  92.08 μs | 23.91 μs | 24.54 |    0.14 |
|             &#39;Check root words&#39; |   324.1 μs |   4.92 μs |  1.28 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   863.8 μs |  15.48 μs |  2.40 μs |  2.67 |    0.01 |
|            &#39;Check wrong words&#39; | 7,266.0 μs | 109.91 μs | 17.01 μs | 22.43 |    0.10 |
