``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-TUTYAF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 23.31 ms | 0.380 ms | 0.059 ms |  0.93 |    0.01 |
|             &#39;Suggest root words&#39; | 25.31 ms | 0.416 ms | 0.248 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 25.90 ms | 0.429 ms | 0.225 ms |  1.02 |    0.01 |
|            &#39;Suggest wrong words&#39; | 29.01 ms | 0.507 ms | 0.302 ms |  1.15 |    0.02 |
