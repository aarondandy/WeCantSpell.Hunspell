``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  Job-YIIEFG : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 25.35 ms | 0.271 ms | 0.097 ms |  0.85 |
|             &#39;Suggest root words&#39; | 29.93 ms | 0.560 ms | 0.087 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 29.80 ms | 0.436 ms | 0.067 ms |  1.00 |
|            &#39;Suggest wrong words&#39; | 33.02 ms | 0.534 ms | 0.083 ms |  1.10 |
