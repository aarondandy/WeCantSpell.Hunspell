``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-XPTSBY : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,824.4 μs | 123.96 μs | 32.19 μs | 18.54 |    0.08 |
|             &#39;Check root words&#39; |   368.1 μs |   5.43 μs |  1.41 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   847.1 μs |  16.69 μs |  4.34 μs |  2.30 |    0.02 |
|            &#39;Check wrong words&#39; | 5,885.9 μs |  90.67 μs | 23.55 μs | 15.99 |    0.11 |
