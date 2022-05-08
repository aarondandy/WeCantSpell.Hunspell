``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-FBWXJG : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 23.09 ms | 0.427 ms | 0.223 ms |  1.00 |    0.01 |
|             &#39;Suggest root words&#39; | 23.19 ms | 0.428 ms | 0.066 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 25.02 ms | 0.350 ms | 0.311 ms |  1.09 |    0.02 |
|            &#39;Suggest wrong words&#39; | 27.54 ms | 0.541 ms | 0.084 ms |  1.19 |    0.00 |
