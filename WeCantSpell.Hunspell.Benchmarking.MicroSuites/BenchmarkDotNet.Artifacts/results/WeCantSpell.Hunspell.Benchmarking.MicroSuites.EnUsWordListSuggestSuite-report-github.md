``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FLBLAG : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 21.91 ms | 0.348 ms | 0.182 ms |  0.92 |    0.02 |
|             &#39;Suggest root words&#39; | 23.98 ms | 0.467 ms | 0.337 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 24.21 ms | 0.422 ms | 0.251 ms |  1.01 |    0.01 |
|            &#39;Suggest wrong words&#39; | 28.13 ms | 0.500 ms | 0.178 ms |  1.17 |    0.02 |
