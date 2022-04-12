``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-AFCXPQ : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.99 ms | 0.206 ms | 0.054 ms |  0.93 |
|             &#39;Suggest root words&#39; | 23.72 ms | 0.264 ms | 0.094 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.90 ms | 0.412 ms | 0.183 ms |  1.09 |
|            &#39;Suggest wrong words&#39; | 28.56 ms | 0.453 ms | 0.237 ms |  1.21 |
