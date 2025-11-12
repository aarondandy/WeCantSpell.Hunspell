```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.7171/24H2/2024Update/HudsonValley)
AMD Ryzen 7 5800H with Radeon Graphics 3.20GHz, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256
  Check en-US : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256

Job=Check en-US  

```
| Method                     | Mean      | Error     | StdDev    | Min       | Median    | Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|------:|
| &#39;Check words: WeCantSpell&#39; | 17.958 ms | 0.0421 ms | 0.0373 ms | 17.896 ms | 17.960 ms |  1.00 |
| &#39;Check words: NHunspell&#39;   |  5.982 ms | 0.0077 ms | 0.0064 ms |  5.968 ms |  5.982 ms |  0.33 |
