``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-DFJJZL : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 25.53 ms | 1.073 ms | 1.236 ms |  0.89 |    0.06 |
|             &#39;Suggest root words&#39; | 28.64 ms | 0.965 ms | 1.111 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 30.16 ms | 1.150 ms | 1.324 ms |  1.05 |    0.06 |
|            &#39;Suggest wrong words&#39; | 33.19 ms | 1.242 ms | 1.430 ms |  1.16 |    0.06 |
