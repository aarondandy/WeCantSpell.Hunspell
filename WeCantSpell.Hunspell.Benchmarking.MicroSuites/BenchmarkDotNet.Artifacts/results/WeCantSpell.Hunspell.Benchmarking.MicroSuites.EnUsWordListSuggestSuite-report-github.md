``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-QRHGPC : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 26.53 ms | 0.459 ms | 0.164 ms |  0.87 |
|             &#39;Suggest root words&#39; | 30.41 ms | 0.566 ms | 0.251 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 30.35 ms | 0.493 ms | 0.258 ms |  1.00 |
|            &#39;Suggest wrong words&#39; | 33.95 ms | 0.525 ms | 0.081 ms |  1.12 |
