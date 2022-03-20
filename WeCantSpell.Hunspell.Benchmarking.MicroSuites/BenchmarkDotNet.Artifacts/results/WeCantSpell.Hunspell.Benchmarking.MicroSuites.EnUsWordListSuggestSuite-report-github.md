``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-PDYCEA : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 26.70 ms | 0.493 ms | 0.356 ms |  0.99 |    0.01 |
|             &#39;Suggest root words&#39; | 26.78 ms | 0.511 ms | 0.227 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 27.20 ms | 0.420 ms | 0.278 ms |  1.02 |    0.01 |
|            &#39;Suggest wrong words&#39; | 32.17 ms | 0.595 ms | 0.393 ms |  1.20 |    0.02 |
