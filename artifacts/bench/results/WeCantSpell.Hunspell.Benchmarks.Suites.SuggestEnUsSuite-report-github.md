```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-FYOECP    : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-ACBKDI    : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Suggest en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256


```
| Method          | Job           | Runtime            | Mean     | Error   | StdDev  | Min      | Median   | Ratio |
|---------------- |-------------- |------------------- |---------:|--------:|--------:|---------:|---------:|------:|
| &#39;Suggest words&#39; | Job-FYOECP    | .NET 6.0           | 421.2 ms | 2.04 ms | 1.59 ms | 418.3 ms | 421.8 ms |  1.00 |
| &#39;Suggest words&#39; | Job-ACBKDI    | .NET 8.0           | 373.3 ms | 0.98 ms | 0.92 ms | 371.6 ms | 373.4 ms |  0.89 |
| &#39;Suggest words&#39; | Suggest en-US | .NET Framework 4.8 | 765.7 ms | 2.13 ms | 1.99 ms | 761.0 ms | 766.3 ms |  1.82 |
