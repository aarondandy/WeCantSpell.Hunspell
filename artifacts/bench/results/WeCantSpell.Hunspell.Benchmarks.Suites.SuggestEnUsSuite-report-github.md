```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]        : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Suggest en-US : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2

Job=Suggest en-US  

```
| Method          | Runtime  | Mean     | Error   | StdDev  | Min      | Median   | Ratio |
|---------------- |--------- |---------:|--------:|--------:|---------:|---------:|------:|
| &#39;Suggest words&#39; | .NET 6.0 | 423.4 ms | 2.19 ms | 2.05 ms | 419.8 ms | 423.1 ms |  1.15 |
| &#39;Suggest words&#39; | .NET 8.0 | 367.2 ms | 1.71 ms | 1.52 ms | 364.5 ms | 367.2 ms |  1.00 |
