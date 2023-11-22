```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-TPWOKF : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

MinInvokeCount=1  IterationTime=1.0000 s  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
| Method                       | Mean    | Error    | StdDev   | Min     | Max     | Median  | Ratio |
|----------------------------- |--------:|---------:|---------:|--------:|--------:|--------:|------:|
| &#39;Suggest words: WeCantSpell&#39; | 1.554 s | 0.0232 s | 0.0060 s | 1.548 s | 1.562 s | 1.553 s |  1.00 |
| &#39;Suggest words: NHunspell&#39;   | 3.766 s | 0.0702 s | 0.0182 s | 3.737 s | 3.785 s | 3.769 s |  2.42 |
