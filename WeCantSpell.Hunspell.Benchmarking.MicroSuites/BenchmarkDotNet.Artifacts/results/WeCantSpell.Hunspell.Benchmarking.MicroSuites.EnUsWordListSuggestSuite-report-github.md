``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-VMTVCK : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.50 ms | 0.363 ms | 0.056 ms |  0.85 |
|             &#39;Suggest root words&#39; | 25.29 ms | 0.350 ms | 0.091 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.78 ms | 0.494 ms | 0.176 ms |  0.98 |
|            &#39;Suggest wrong words&#39; | 28.55 ms | 0.555 ms | 0.086 ms |  1.13 |
