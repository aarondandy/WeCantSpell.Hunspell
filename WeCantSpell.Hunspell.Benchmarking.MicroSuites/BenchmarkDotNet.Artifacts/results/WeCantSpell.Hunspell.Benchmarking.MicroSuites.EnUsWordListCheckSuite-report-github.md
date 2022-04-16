``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-YSYWSA : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,059.5 μs | 131.85 μs |  7.23 μs | 18.94 |    0.09 |
|             &#39;Check root words&#39; |   373.2 μs |   5.68 μs |  1.48 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   867.2 μs |   7.42 μs |  1.15 μs |  2.32 |    0.01 |
|            &#39;Check wrong words&#39; | 6,007.1 μs |  86.61 μs | 22.49 μs | 16.10 |    0.12 |
