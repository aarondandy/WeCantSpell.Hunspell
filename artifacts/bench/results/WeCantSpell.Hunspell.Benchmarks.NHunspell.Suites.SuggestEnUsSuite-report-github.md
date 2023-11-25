```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean       | Error   | StdDev  | Min        | Median     | Ratio |
|----------------------------- |-----------:|--------:|--------:|-----------:|-----------:|------:|
| &#39;Suggest words: WeCantSpell&#39; |   760.4 ms | 2.05 ms | 1.71 ms |   757.6 ms |   760.4 ms |  1.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,922.0 ms | 4.96 ms | 4.64 ms | 1,913.7 ms | 1,922.0 ms |  2.53 |
