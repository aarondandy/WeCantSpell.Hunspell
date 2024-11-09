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
| &#39;Suggest words&#39; | 357.9 ms | 2.12 ms | 1.98 ms | 353.4 ms | 357.7 ms |  1.00 |
