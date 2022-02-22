``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-BVUGPX : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.69 ms | 0.395 ms | 0.103 ms |  0.85 |
|             &#39;Suggest root words&#39; | 30.13 ms | 0.280 ms | 0.015 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.84 ms | 0.448 ms | 0.069 ms |  0.99 |
|            &#39;Suggest wrong words&#39; | 33.79 ms | 0.534 ms | 0.139 ms |  1.12 |
