``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-LZSJPM : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 22.84 ms | 0.449 ms | 0.441 ms |  0.91 |    0.02 |
|             &#39;Suggest root words&#39; | 25.08 ms | 0.453 ms | 0.269 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 25.68 ms | 0.395 ms | 0.175 ms |  1.03 |    0.01 |
|            &#39;Suggest wrong words&#39; | 29.45 ms | 0.507 ms | 0.396 ms |  1.18 |    0.02 |
