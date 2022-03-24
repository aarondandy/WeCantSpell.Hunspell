``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-BIZISB : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 23.21 ms | 0.448 ms | 0.160 ms |  0.94 |    0.01 |
|             &#39;Suggest root words&#39; | 24.78 ms | 0.452 ms | 0.237 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 25.48 ms | 0.485 ms | 0.288 ms |  1.03 |    0.02 |
|            &#39;Suggest wrong words&#39; | 29.63 ms | 0.436 ms | 0.067 ms |  1.19 |    0.01 |
