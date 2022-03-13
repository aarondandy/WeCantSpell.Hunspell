``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-VMTVCK : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|----------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 8,404.0 μs | 149.94 μs | 108.41 μs | 28.91 |    0.44 |
|             &#39;Check root words&#39; |   290.6 μs |   5.77 μs |   0.89 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   819.8 μs |  11.71 μs |   4.18 μs |  2.82 |    0.01 |
|            &#39;Check wrong words&#39; | 7,250.8 μs | 105.60 μs |  37.66 μs | 24.94 |    0.11 |
