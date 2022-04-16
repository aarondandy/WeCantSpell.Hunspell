``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FLBLAG : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,916.4 μs | 114.26 μs | 50.73 μs | 18.57 |    0.13 |
|             &#39;Check root words&#39; |   372.2 μs |   6.42 μs |  0.99 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   850.5 μs |  14.37 μs |  2.22 μs |  2.29 |    0.01 |
|            &#39;Check wrong words&#39; | 5,888.9 μs | 112.64 μs | 17.43 μs | 15.82 |    0.07 |
