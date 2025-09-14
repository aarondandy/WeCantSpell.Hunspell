```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 10.0.100-rc.1.25451.107
  [Host]        : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2
  Suggest en-US : .NET 10.0.0 (10.0.25.45207), X64 RyuJIT AVX2

Job=Suggest en-US  

```
| Method          | Runtime   | Mean    | Error    | StdDev   | Min     | Median  | Ratio |
|---------------- |---------- |--------:|---------:|---------:|--------:|--------:|------:|
| &#39;Suggest words&#39; | .NET 10.0 | 1.429 s | 0.0036 s | 0.0033 s | 1.424 s | 1.428 s |  0.97 |
| &#39;Suggest words&#39; | .NET 9.0  | 1.477 s | 0.0086 s | 0.0077 s | 1.459 s | 1.476 s |  1.00 |
