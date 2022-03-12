``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-ZEKWHL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,388.4 μs | 139.63 μs |  62.00 μs | 28.21 |    0.95 |
|             &#39;Check root words&#39; |   297.1 μs |   8.21 μs |   9.13 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   873.3 μs |  21.81 μs |  24.24 μs |  2.94 |    0.12 |
|            &#39;Check wrong words&#39; | 7,482.7 μs | 149.21 μs | 124.59 μs | 25.07 |    1.00 |
