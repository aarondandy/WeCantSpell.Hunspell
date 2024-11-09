```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9277.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9277.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean       | Error   | StdDev  | Min        | Median     | Ratio |
|----------------------------- |-----------:|--------:|--------:|-----------:|-----------:|------:|
| &#39;Suggest words: WeCantSpell&#39; |   807.2 ms | 1.51 ms | 1.26 ms |   804.7 ms |   807.3 ms |  1.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,903.5 ms | 3.81 ms | 3.18 ms | 1,897.6 ms | 1,903.4 ms |  2.36 |
