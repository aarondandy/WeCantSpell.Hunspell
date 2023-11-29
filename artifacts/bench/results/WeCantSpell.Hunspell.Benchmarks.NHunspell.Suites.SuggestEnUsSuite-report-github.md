```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean       | Error    | StdDev   | Min        | Median     | Ratio | RatioSD |
|----------------------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; |   744.9 ms |  1.39 ms |  1.23 ms |   743.1 ms |   744.9 ms |  1.00 |    0.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,907.2 ms | 13.80 ms | 12.23 ms | 1,871.7 ms | 1,912.2 ms |  2.56 |    0.02 |
