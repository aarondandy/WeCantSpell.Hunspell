``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-VSAEEK : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,627.5 μs |  70.86 μs | 10.97 μs | 19.14 |    0.05 |
|             &#39;Check root words&#39; |   345.9 μs |   4.90 μs |  1.27 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   791.1 μs |  14.86 μs |  3.86 μs |  2.29 |    0.01 |
|            &#39;Check wrong words&#39; | 5,880.1 μs | 104.93 μs | 37.42 μs | 17.01 |    0.15 |
