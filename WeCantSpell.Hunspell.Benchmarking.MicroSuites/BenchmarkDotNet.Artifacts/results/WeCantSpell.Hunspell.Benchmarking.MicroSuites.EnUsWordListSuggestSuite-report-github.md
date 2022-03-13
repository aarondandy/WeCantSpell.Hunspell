``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-CUPMZA : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 21.55 ms | 0.422 ms | 0.110 ms |  0.86 |    0.02 |
|             &#39;Suggest root words&#39; | 24.63 ms | 0.486 ms | 0.540 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 25.19 ms | 0.400 ms | 0.062 ms |  1.00 |    0.02 |
|            &#39;Suggest wrong words&#39; | 28.13 ms | 0.370 ms | 0.057 ms |  1.12 |    0.03 |
