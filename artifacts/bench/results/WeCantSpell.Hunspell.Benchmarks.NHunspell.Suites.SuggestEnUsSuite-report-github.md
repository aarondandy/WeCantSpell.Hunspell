```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
AMD Ryzen 7 5800H with Radeon Graphics 3.20GHz, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean    | Error    | StdDev   | Min     | Median  | Ratio | RatioSD |
|----------------------------- |--------:|---------:|---------:|--------:|--------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; | 3.595 s | 0.0637 s | 0.0497 s | 3.572 s | 3.578 s |  1.00 |    0.02 |
| &#39;Suggest words: NHunspell&#39;   | 8.865 s | 0.1756 s | 0.3211 s | 8.161 s | 8.858 s |  2.47 |    0.09 |
