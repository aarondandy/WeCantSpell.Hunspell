``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-SDFLID : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 22.51 ms | 0.340 ms | 0.151 ms |  0.90 |
|             &#39;Suggest root words&#39; | 24.98 ms | 0.387 ms | 0.138 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 25.36 ms | 0.451 ms | 0.269 ms |  1.01 |
|            &#39;Suggest wrong words&#39; | 28.60 ms | 0.557 ms | 0.291 ms |  1.14 |
