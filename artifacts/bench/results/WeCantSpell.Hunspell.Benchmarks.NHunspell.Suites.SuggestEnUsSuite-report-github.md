```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean       | Error    | StdDev   | Min        | Median     | Ratio | RatioSD |
|----------------------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; |   753.2 ms |  1.85 ms |  1.64 ms |   750.7 ms |   752.7 ms |  1.00 |    0.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,910.4 ms | 15.15 ms | 12.65 ms | 1,882.0 ms | 1,914.9 ms |  2.54 |    0.02 |
