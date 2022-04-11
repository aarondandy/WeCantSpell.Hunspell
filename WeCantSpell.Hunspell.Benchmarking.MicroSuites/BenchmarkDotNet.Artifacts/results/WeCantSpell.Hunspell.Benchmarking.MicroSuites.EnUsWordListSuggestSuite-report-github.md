``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1620 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-OKTOJG : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 22.48 ms | 0.400 ms | 0.312 ms |  0.93 |    0.01 |
|             &#39;Suggest root words&#39; | 24.04 ms | 0.425 ms | 0.189 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 25.30 ms | 0.494 ms | 0.327 ms |  1.05 |    0.02 |
|            &#39;Suggest wrong words&#39; | 27.66 ms | 0.369 ms | 0.057 ms |  1.15 |    0.01 |
