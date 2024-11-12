```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9277.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9277.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean       | Error    | StdDev   | Min        | Median     | Ratio | RatioSD |
|----------------------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; |   818.8 ms |  3.59 ms |  2.81 ms |   815.6 ms |   817.8 ms |  1.00 |    0.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,907.3 ms | 20.64 ms | 17.24 ms | 1,875.9 ms | 1,904.0 ms |  2.33 |    0.02 |
