``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ANGCFT : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.05 ms | 0.378 ms | 0.059 ms |  0.91 |
|             &#39;Suggest root words&#39; | 24.09 ms | 0.465 ms | 0.166 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.49 ms | 0.464 ms | 0.072 ms |  1.01 |
|            &#39;Suggest wrong words&#39; | 27.40 ms | 0.483 ms | 0.253 ms |  1.14 |
