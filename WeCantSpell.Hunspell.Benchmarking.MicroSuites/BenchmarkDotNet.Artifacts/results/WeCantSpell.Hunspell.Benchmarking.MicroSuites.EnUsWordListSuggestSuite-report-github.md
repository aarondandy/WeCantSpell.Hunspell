``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-LIFDMP : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 21.96 ms | 0.390 ms | 0.258 ms |  0.90 |
|             &#39;Suggest root words&#39; | 24.30 ms | 0.402 ms | 0.104 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 24.92 ms | 0.347 ms | 0.090 ms |  1.03 |
|            &#39;Suggest wrong words&#39; | 27.85 ms | 0.521 ms | 0.272 ms |  1.15 |
