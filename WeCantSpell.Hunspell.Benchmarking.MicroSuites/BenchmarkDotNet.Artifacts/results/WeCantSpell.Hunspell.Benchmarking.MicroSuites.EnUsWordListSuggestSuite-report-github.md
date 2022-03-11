``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-MIJBKN : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 26.89 ms | 0.537 ms | 0.419 ms |  0.84 |    0.02 |
|             &#39;Suggest root words&#39; | 32.30 ms | 0.609 ms | 0.094 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 30.14 ms | 0.412 ms | 0.064 ms |  0.93 |    0.00 |
|            &#39;Suggest wrong words&#39; | 33.63 ms | 0.134 ms | 0.007 ms |  1.04 |    0.00 |
