```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.403
  [Host]        : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  Suggest en-US : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2

Job=Suggest en-US  Runtime=.NET 8.0  

```
| Method          | Mean     | Error   | StdDev  | Min      | Median   | Ratio |
|---------------- |---------:|--------:|--------:|---------:|---------:|------:|
| &#39;Suggest words&#39; | 362.5 ms | 1.72 ms | 1.44 ms | 360.7 ms | 361.8 ms |  1.00 |
