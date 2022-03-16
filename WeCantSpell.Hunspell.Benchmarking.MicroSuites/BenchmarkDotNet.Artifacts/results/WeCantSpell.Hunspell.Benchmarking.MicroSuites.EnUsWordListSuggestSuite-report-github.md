``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  Job-UZIGPF : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio |
|--------------------------------- |---------:|---------:|---------:|------:|
| &#39;Suggest an assortment of words&#39; | 19.40 ms | 0.266 ms | 0.041 ms |  0.89 |
|             &#39;Suggest root words&#39; | 21.78 ms | 0.148 ms | 0.023 ms |  1.00 |
|          &#39;Suggest correct words&#39; | 22.34 ms | 0.414 ms | 0.147 ms |  1.02 |
|            &#39;Suggest wrong words&#39; | 25.36 ms | 0.444 ms | 0.197 ms |  1.16 |
