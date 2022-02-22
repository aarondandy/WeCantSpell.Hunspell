``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-IJZYCM : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,517.6 μs | 112.92 μs | 29.33 μs | 29.39 |    0.15 |
|             &#39;Check root words&#39; |   255.8 μs |   3.00 μs |  0.78 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   766.4 μs |  11.50 μs |  1.78 μs |  3.00 |    0.02 |
|            &#39;Check wrong words&#39; | 6,673.8 μs | 122.84 μs | 19.01 μs | 26.08 |    0.12 |
