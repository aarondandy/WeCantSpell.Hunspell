```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.7171/24H2/2024Update/HudsonValley)
AMD Ryzen 7 5800H with Radeon Graphics 3.20GHz, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean    | Error    | StdDev   | Min     | Median  | Ratio | RatioSD |
|----------------------------- |--------:|---------:|---------:|--------:|--------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; | 3.735 s | 0.0740 s | 0.1461 s | 3.510 s | 3.701 s |  1.00 |    0.05 |
| &#39;Suggest words: NHunspell&#39;   | 8.219 s | 0.1331 s | 0.1180 s | 7.989 s | 8.252 s |  2.20 |    0.09 |
