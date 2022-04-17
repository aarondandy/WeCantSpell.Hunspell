``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  Job-ZRRUOX : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
|                           Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------------------- |---------:|---------:|---------:|------:|--------:|
| &#39;Suggest an assortment of words&#39; | 21.56 ms | 0.351 ms | 0.091 ms |  0.91 |    0.01 |
|             &#39;Suggest root words&#39; | 23.60 ms | 0.271 ms | 0.161 ms |  1.00 |    0.00 |
|          &#39;Suggest correct words&#39; | 24.97 ms | 0.479 ms | 0.346 ms |  1.06 |    0.02 |
|            &#39;Suggest wrong words&#39; | 28.14 ms | 0.535 ms | 0.029 ms |  1.19 |    0.01 |
