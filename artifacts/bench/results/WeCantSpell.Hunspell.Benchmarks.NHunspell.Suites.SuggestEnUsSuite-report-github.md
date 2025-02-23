```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3194)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9290.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9290.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean    | Error    | StdDev   | Min     | Median  | Ratio | RatioSD |
|----------------------------- |--------:|---------:|---------:|--------:|--------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; | 3.559 s | 0.0043 s | 0.0038 s | 3.554 s | 3.558 s |  1.00 |    0.00 |
| &#39;Suggest words: NHunspell&#39;   | 7.849 s | 0.0899 s | 0.0999 s | 7.731 s | 7.877 s |  2.21 |    0.03 |
