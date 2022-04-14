``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ESYPON : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.16 ms | 0.273 ms | 0.121 ms |  0.88 |
|             &#39;Suggest root words&#39; | 25.06 ms | 0.452 ms | 0.161 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.50 ms | 0.475 ms | 0.248 ms |  1.02 |
|            &#39;Suggest wrong words&#39; | 28.20 ms | 0.468 ms | 0.245 ms |  1.13 |
