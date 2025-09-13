```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
AMD Ryzen 7 5800H with Radeon Graphics 3.20GHz, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean    | Error    | StdDev   | Min     | Median  | Ratio |
|----------------------------- |--------:|---------:|---------:|--------:|--------:|------:|
| &#39;Suggest words: WeCantSpell&#39; | 3.570 s | 0.0025 s | 0.0023 s | 3.565 s | 3.570 s |  1.00 |
| &#39;Suggest words: NHunspell&#39;   | 7.777 s | 0.0242 s | 0.0189 s | 7.719 s | 7.783 s |  2.18 |
