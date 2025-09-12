```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.305
  [Host]        : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Suggest en-US : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=Suggest en-US  Runtime=.NET 9.0  

```
| Method          | Mean    | Error    | StdDev   | Min     | Median  | Ratio |
|---------------- |--------:|---------:|---------:|--------:|--------:|------:|
| &#39;Suggest words&#39; | 1.455 s | 0.0040 s | 0.0038 s | 1.449 s | 1.454 s |  1.00 |
