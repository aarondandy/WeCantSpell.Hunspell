``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ZRRUOX : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 6,913.6 μs | 127.19 μs | 19.68 μs | 18.59 |    0.10 |
|             &#39;Check root words&#39; |   372.7 μs |   5.69 μs |  2.53 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   860.9 μs |  13.07 μs |  5.80 μs |  2.31 |    0.02 |
|            &#39;Check wrong words&#39; | 5,867.4 μs | 115.72 μs | 17.91 μs | 15.78 |    0.08 |
