```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]        : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-FYOECP    : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-ACBKDI    : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Suggest en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256


```
| Method          | Job           | Runtime            | Mean     | Error   | StdDev   | Min      | Median   | Ratio | RatioSD |
|---------------- |-------------- |------------------- |---------:|--------:|---------:|---------:|---------:|------:|--------:|
| &#39;Suggest words&#39; | Job-FYOECP    | .NET 6.0           | 405.1 ms | 0.82 ms |  0.68 ms | 404.0 ms | 405.2 ms |  1.00 |    0.00 |
| &#39;Suggest words&#39; | Job-ACBKDI    | .NET 8.0           | 366.4 ms | 0.77 ms |  0.72 ms | 365.5 ms | 366.6 ms |  0.90 |    0.00 |
| &#39;Suggest words&#39; | Suggest en-US | .NET Framework 4.8 | 750.5 ms | 8.75 ms | 12.27 ms | 738.6 ms | 746.7 ms |  1.87 |    0.03 |
