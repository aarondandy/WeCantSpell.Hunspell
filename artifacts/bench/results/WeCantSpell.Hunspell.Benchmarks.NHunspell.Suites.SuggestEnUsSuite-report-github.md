```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9282.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9282.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean    | Error    | StdDev   | Min     | Median  | Ratio |
|----------------------------- |--------:|---------:|---------:|--------:|--------:|------:|
| &#39;Suggest words: WeCantSpell&#39; | 3.580 s | 0.0110 s | 0.0103 s | 3.569 s | 3.577 s |  1.00 |
| &#39;Suggest words: NHunspell&#39;   | 7.781 s | 0.0529 s | 0.0469 s | 7.686 s | 7.774 s |  2.17 |
