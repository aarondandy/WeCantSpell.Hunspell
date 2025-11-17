```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.7171/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100
  [Host]        : .NET 10.0.0 (10.0.25.52411), X64 RyuJIT AVX2
  Suggest en-US : .NET 10.0.0 (10.0.25.52411), X64 RyuJIT AVX2

Job=Suggest en-US  

```
| Method          | Runtime   | Mean    | Error    | StdDev   | Min     | Median  | Ratio |
|---------------- |---------- |--------:|---------:|---------:|--------:|--------:|------:|
| &#39;Suggest words&#39; | .NET 10.0 | 1.438 s | 0.0025 s | 0.0022 s | 1.435 s | 1.438 s |  0.95 |
| &#39;Suggest words&#39; | .NET 9.0  | 1.519 s | 0.0024 s | 0.0022 s | 1.516 s | 1.519 s |  1.00 |
