``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-DFUDWF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 25.99 ms | 0.492 ms | 0.325 ms |  0.83 |    0.02 |
|             &#39;Suggest root words&#39; | 31.33 ms | 0.438 ms | 0.449 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 30.00 ms | 0.266 ms | 0.041 ms |  0.95 |    0.01 |
|            &#39;Suggest wrong words&#39; | 33.40 ms | 0.304 ms | 0.047 ms |  1.06 |    0.01 |
