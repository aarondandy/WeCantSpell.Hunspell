```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]        : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  Suggest en-US : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2

Job=Suggest en-US  

```
| Method          | Runtime  | Mean    | Error    | StdDev   | Min     | Median  | Ratio | RatioSD |
|---------------- |--------- |--------:|---------:|---------:|--------:|--------:|------:|--------:|
| &#39;Suggest words&#39; | .NET 8.0 | 1.569 s | 0.0044 s | 0.0039 s | 1.563 s | 1.569 s |  1.00 |    0.00 |
| &#39;Suggest words&#39; | .NET 9.0 | 1.545 s | 0.0298 s | 0.0279 s | 1.518 s | 1.532 s |  0.99 |    0.02 |
