``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-JWILFL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 23.42 ms | 0.329 ms | 0.218 ms |  0.90 |
|             &#39;Suggest root words&#39; | 26.02 ms | 0.479 ms | 0.125 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 26.97 ms | 0.539 ms | 0.282 ms |  1.04 |
|            &#39;Suggest wrong words&#39; | 29.31 ms | 0.331 ms | 0.197 ms |  1.12 |
