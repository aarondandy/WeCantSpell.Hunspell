``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FKDBBQ : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,192.4 μs |  46.63 μs |  7.22 μs | 17.81 |    0.07 |
|             &#39;Check root words&#39; |   404.0 μs |   7.13 μs |  1.85 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   906.7 μs |  14.83 μs |  0.81 μs |  2.24 |    0.01 |
|            &#39;Check wrong words&#39; | 6,177.4 μs | 106.60 μs | 16.50 μs | 15.30 |    0.09 |
