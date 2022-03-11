``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-VBVQFN : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.64 ms | 0.199 ms | 0.031 ms |  0.85 |
|             &#39;Suggest root words&#39; | 30.05 ms | 0.547 ms | 0.142 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 30.05 ms | 0.349 ms | 0.091 ms |  1.00 |
|            &#39;Suggest wrong words&#39; | 34.87 ms | 0.526 ms | 0.081 ms |  1.16 |
