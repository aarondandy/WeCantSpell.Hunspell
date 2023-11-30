```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Suggest en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256

Job=Suggest en-US  

```
| Method                       | Mean       | Error    | StdDev   | Min        | Median     | Ratio | RatioSD |
|----------------------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|--------:|
| &#39;Suggest words: WeCantSpell&#39; |   738.5 ms |  7.00 ms |  5.47 ms |   729.7 ms |   739.3 ms |  1.00 |    0.00 |
| &#39;Suggest words: NHunspell&#39;   | 1,905.4 ms | 14.34 ms | 11.20 ms | 1,879.8 ms | 1,904.4 ms |  2.58 |    0.02 |
