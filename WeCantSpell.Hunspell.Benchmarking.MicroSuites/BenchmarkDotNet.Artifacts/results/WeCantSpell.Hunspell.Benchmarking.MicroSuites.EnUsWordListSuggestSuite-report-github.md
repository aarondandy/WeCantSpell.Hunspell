``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-RMGOJL : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 26.76 ms | 0.470 ms | 0.246 ms |  0.89 |    0.01 |
|             &#39;Suggest root words&#39; | 30.01 ms | 0.511 ms | 0.399 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 31.01 ms | 0.611 ms | 0.159 ms |  1.03 |    0.01 |
|            &#39;Suggest wrong words&#39; | 33.93 ms | 0.627 ms | 0.415 ms |  1.13 |    0.02 |
