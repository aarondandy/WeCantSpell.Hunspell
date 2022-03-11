``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-FFWCQF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 25.25 ms | 0.497 ms | 0.129 ms |  0.84 |    0.01 |
|             &#39;Suggest root words&#39; | 30.01 ms | 0.582 ms | 0.151 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 30.16 ms | 0.601 ms | 0.532 ms |  1.01 |    0.02 |
|            &#39;Suggest wrong words&#39; | 34.65 ms | 0.688 ms | 0.179 ms |  1.15 |    0.01 |
