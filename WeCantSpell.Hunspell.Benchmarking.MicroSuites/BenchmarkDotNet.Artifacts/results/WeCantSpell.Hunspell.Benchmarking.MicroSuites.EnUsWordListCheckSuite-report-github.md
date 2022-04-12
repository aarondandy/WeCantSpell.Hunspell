``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-AFCXPQ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,189.2 μs | 122.24 μs | 54.27 μs | 17.73 |    0.14 |
|             &#39;Check root words&#39; |   406.3 μs |   5.57 μs |  1.45 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   921.6 μs |  13.67 μs |  0.75 μs |  2.27 |    0.01 |
|            &#39;Check wrong words&#39; | 6,177.8 μs |  34.45 μs |  1.89 μs | 15.22 |    0.08 |
