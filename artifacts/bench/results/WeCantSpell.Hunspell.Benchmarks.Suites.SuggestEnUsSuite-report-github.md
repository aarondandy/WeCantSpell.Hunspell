```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.403
  [Host]        : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  Suggest en-US : .NET 6.0.35 (6.0.3524.45918), X64 RyuJIT AVX2

Job=Suggest en-US  

```
| Method          | Runtime  | Mean     | Error   | StdDev  | Min      | Median   | Ratio |
|---------------- |--------- |---------:|--------:|--------:|---------:|---------:|------:|
| &#39;Suggest words&#39; | .NET 6.0 | 408.6 ms | 2.37 ms | 2.10 ms | 405.1 ms | 408.8 ms |  1.12 |
| &#39;Suggest words&#39; | .NET 8.0 | 364.9 ms | 1.91 ms | 1.79 ms | 361.4 ms | 364.8 ms |  1.00 |
