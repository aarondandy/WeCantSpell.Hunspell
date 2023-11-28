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
| &#39;Suggest words&#39; | Job-FYOECP    | .NET 6.0           | 417.8 ms | 2.05 ms | 1.82 ms | 414.8 ms | 417.7 ms |  1.00 |
| &#39;Suggest words&#39; | Job-ACBKDI    | .NET 8.0           | 380.6 ms | 2.60 ms | 2.43 ms | 377.3 ms | 380.1 ms |  0.91 |
| &#39;Suggest words&#39; | Suggest en-US | .NET Framework 4.8 | 757.1 ms | 1.73 ms | 1.62 ms | 753.9 ms | 757.1 ms |  1.81 |
