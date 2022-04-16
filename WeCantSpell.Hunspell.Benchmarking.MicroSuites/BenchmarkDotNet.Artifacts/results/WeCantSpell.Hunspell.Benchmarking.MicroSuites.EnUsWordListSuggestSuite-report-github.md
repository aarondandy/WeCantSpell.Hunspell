``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-YSYWSA : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 21.67 ms | 0.368 ms | 0.192 ms |  0.90 |    0.00 |
|             &#39;Suggest root words&#39; | 24.03 ms | 0.396 ms | 0.207 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 24.70 ms | 0.475 ms | 0.344 ms |  1.03 |    0.02 |
|            &#39;Suggest wrong words&#39; | 27.36 ms | 0.527 ms | 0.234 ms |  1.14 |    0.01 |
