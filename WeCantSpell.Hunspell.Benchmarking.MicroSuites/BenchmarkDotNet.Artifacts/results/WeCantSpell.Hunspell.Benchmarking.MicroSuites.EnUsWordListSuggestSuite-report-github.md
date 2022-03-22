``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TEWBAJ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 23.82 ms | 0.397 ms | 0.236 ms |  0.95 |
|             &#39;Suggest root words&#39; | 25.04 ms | 0.482 ms | 0.214 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.88 ms | 0.460 ms | 0.204 ms |  1.03 |
|            &#39;Suggest wrong words&#39; | 30.34 ms | 0.448 ms | 0.116 ms |  1.21 |
