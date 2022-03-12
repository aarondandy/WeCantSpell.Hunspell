``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-ZSNNIG : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 9,018.0 μs | 325.09 μs | 333.85 μs | 28.74 |    0.44 |
|             &#39;Check root words&#39; |   321.8 μs |   4.78 μs |   0.74 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   896.9 μs |  18.49 μs |  19.79 μs |  2.79 |    0.06 |
|            &#39;Check wrong words&#39; | 7,503.2 μs | 127.49 μs |  66.68 μs | 23.27 |    0.25 |
