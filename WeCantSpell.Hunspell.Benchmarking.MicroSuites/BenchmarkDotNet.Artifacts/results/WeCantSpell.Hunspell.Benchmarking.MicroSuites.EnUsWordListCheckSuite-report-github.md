``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-DKMGTA : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 9,082.7 μs | 264.17 μs | 304.22 μs | 26.70 |    1.03 |
|             &#39;Check root words&#39; |   346.7 μs |   5.19 μs |   0.80 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   936.5 μs |  16.72 μs |  18.59 μs |  2.69 |    0.07 |
|            &#39;Check wrong words&#39; | 7,928.6 μs | 150.96 μs | 161.53 μs | 22.53 |    0.36 |
