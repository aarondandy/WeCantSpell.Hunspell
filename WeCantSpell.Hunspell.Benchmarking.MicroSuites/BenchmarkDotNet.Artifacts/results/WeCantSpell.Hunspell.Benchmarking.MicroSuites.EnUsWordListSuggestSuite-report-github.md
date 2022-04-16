``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-NUBGWY : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.01 ms | 0.384 ms | 0.201 ms |  0.93 |
|             &#39;Suggest root words&#39; | 23.76 ms | 0.444 ms | 0.158 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.12 ms | 0.192 ms | 0.030 ms |  1.06 |
|            &#39;Suggest wrong words&#39; | 29.61 ms | 0.549 ms | 0.142 ms |  1.25 |
