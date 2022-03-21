``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TUTYAF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                         Method |       Mean |     Error |   StdDev | Ratio | RatioSD |
|------------------------------- |-----------:|----------:|---------:|------:|--------:|
| &#39;Check an assortment of words&#39; | 7,932.5 μs | 102.97 μs | 36.72 μs | 24.32 |    0.27 |
|             &#39;Check root words&#39; |   325.8 μs |   5.72 μs |  2.54 μs |  1.00 |    0.00 |
|          &#39;Check correct words&#39; |   846.3 μs |  12.98 μs |  3.37 μs |  2.59 |    0.03 |
|            &#39;Check wrong words&#39; | 7,007.8 μs | 102.06 μs | 15.79 μs | 21.49 |    0.20 |
