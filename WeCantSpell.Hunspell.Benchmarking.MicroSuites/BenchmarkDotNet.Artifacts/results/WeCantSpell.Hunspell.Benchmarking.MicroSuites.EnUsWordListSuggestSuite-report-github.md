``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FJUBKD : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 23.60 ms | 0.351 ms | 0.091 ms |  0.95 |
|             &#39;Suggest root words&#39; | 24.88 ms | 0.330 ms | 0.118 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.43 ms | 0.440 ms | 0.230 ms |  1.02 |
|            &#39;Suggest wrong words&#39; | 28.61 ms | 0.502 ms | 0.263 ms |  1.15 |
