``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-ZEKWHL : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.72 ms | 0.487 ms | 0.255 ms |  0.84 |
|             &#39;Suggest root words&#39; | 30.88 ms | 0.394 ms | 0.061 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.90 ms | 0.304 ms | 0.017 ms |  0.97 |
|            &#39;Suggest wrong words&#39; | 33.38 ms | 0.323 ms | 0.050 ms |  1.08 |
