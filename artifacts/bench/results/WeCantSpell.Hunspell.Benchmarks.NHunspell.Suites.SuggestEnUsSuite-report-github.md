```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3194)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9290.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9290.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean    | Error    | StdDev   | Min     | Median  | Ratio | RatioSD |
|----------------------------- |--------:|---------:|---------:|--------:|--------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; | 3.470 s | 0.0041 s | 0.0037 s | 3.464 s | 3.470 s |  1.00 |    0.00 |
| &#39;Suggest words: NHunspell&#39;   | 7.795 s | 0.1459 s | 0.1293 s | 7.701 s | 7.744 s |  2.25 |    0.04 |
