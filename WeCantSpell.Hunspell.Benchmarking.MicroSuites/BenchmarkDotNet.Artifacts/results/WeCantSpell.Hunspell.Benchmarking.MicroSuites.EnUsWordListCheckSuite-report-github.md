``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-QYTTQF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,255.2 μs | 130.32 μs | 46.47 μs | 17.63 |    0.09 |
|             &#39;Check root words&#39; |   411.9 μs |   7.19 μs |  1.87 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   918.5 μs |  15.35 μs |  8.03 μs |  2.23 |    0.03 |
|            &#39;Check wrong words&#39; | 6,398.8 μs |  81.13 μs | 21.07 μs | 15.54 |    0.11 |
