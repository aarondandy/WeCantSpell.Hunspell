``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-OVLAKM : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.36 ms | 0.436 ms | 0.260 ms |  0.90 |
|             &#39;Suggest root words&#39; | 24.88 ms | 0.428 ms | 0.224 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.38 ms | 0.400 ms | 0.178 ms |  1.02 |
|            &#39;Suggest wrong words&#39; | 28.35 ms | 0.393 ms | 0.102 ms |  1.14 |
