``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FFWCQF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,022.2 μs | 146.47 μs | 22.67 μs | 28.32 |    0.23 |
|             &#39;Check root words&#39; |   283.8 μs |   5.04 μs |  2.24 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   816.0 μs |  11.62 μs |  3.02 μs |  2.88 |    0.02 |
|            &#39;Check wrong words&#39; | 7,066.4 μs | 134.61 μs | 48.00 μs | 24.87 |    0.32 |
