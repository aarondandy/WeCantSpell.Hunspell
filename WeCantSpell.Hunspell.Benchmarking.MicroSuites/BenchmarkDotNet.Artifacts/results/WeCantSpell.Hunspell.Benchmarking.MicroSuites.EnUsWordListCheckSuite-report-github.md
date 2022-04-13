``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-DFJJZL : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,687.2 μs | 150.87 μs | 154.93 μs | 17.97 |    0.54 |
|             &#39;Check root words&#39; |   428.2 μs |   8.20 μs |   8.77 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   957.6 μs |  18.15 μs |  16.98 μs |  2.24 |    0.07 |
|            &#39;Check wrong words&#39; | 6,623.1 μs | 118.76 μs |  99.17 μs | 15.55 |    0.34 |
