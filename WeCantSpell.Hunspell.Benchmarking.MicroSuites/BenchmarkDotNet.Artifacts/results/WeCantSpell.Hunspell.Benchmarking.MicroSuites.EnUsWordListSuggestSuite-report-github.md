``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-VYVIDP : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 19.53 ms | 0.195 ms | 0.087 ms |  0.87 |    0.00 |
|             &#39;Suggest root words&#39; | 22.40 ms | 0.172 ms | 0.114 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 22.73 ms | 0.252 ms | 0.236 ms |  1.02 |    0.01 |
|            &#39;Suggest wrong words&#39; | 25.57 ms | 0.504 ms | 0.300 ms |  1.14 |    0.02 |
