```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-FYOECP    : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-ACBKDI    : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Suggest en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256


```
| Method          | Job           | Runtime            | Mean     | Error   | StdDev  | Min      | Median   | Ratio | RatioSD |
|---------------- |-------------- |------------------- |---------:|--------:|--------:|---------:|---------:|------:|--------:|
| &#39;Suggest words&#39; | Job-FYOECP    | .NET 6.0           | 411.8 ms | 1.68 ms | 1.41 ms | 408.9 ms | 412.0 ms |  1.00 |    0.00 |
| &#39;Suggest words&#39; | Job-ACBKDI    | .NET 8.0           | 380.3 ms | 7.49 ms | 7.36 ms | 370.7 ms | 379.9 ms |  0.92 |    0.02 |
| &#39;Suggest words&#39; | Suggest en-US | .NET Framework 4.8 | 773.8 ms | 4.86 ms | 4.55 ms | 766.6 ms | 773.6 ms |  1.88 |    0.01 |
