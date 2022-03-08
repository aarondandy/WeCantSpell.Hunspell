``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-VUHATB : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,850.1 μs | 140.15 μs |  36.40 μs | 27.22 |    0.15 |
|             &#39;Check root words&#39; |   288.5 μs |   3.33 μs |   0.51 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   819.4 μs |   9.23 μs |   1.43 μs |  2.84 |    0.00 |
|            &#39;Check wrong words&#39; | 6,929.3 μs | 132.99 μs | 103.83 μs | 23.81 |    0.16 |
