```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-FYOECP  : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-ACBKDI  : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Check en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256


```
| Method        | Job         | Runtime            | set     | words        | Mean        | Error    | StdDev   | Min         | Median      | Ratio |
|-------------- |------------ |------------------- |-------- |------------- |------------:|---------:|---------:|------------:|------------:|------:|
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **All**     | **String[7158]** | **10,127.9 μs** | **52.43 μs** | **43.78 μs** | **10,038.9 μs** | **10,134.1 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | All     | String[7158] |  7,928.0 μs | 15.83 μs | 14.81 μs |  7,887.6 μs |  7,932.9 μs |  0.78 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | All     | String[7158] | 18,337.3 μs | 70.02 μs | 62.07 μs | 18,152.5 μs | 18,342.9 μs |  1.81 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Correct** | **String[3001]** |    **828.0 μs** |  **0.98 μs** |  **0.87 μs** |    **826.9 μs** |    **827.8 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Correct | String[3001] |    544.7 μs |  1.94 μs |  1.82 μs |    539.8 μs |    545.0 μs |  0.66 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Correct | String[3001] |  1,415.8 μs |  3.65 μs |  3.24 μs |  1,411.5 μs |  1,415.5 μs |  1.71 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Roots**   | **String[2035]** |    **411.8 μs** |  **1.19 μs** |  **1.11 μs** |    **409.3 μs** |    **411.7 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Roots   | String[2035] |    260.7 μs |  1.31 μs |  1.22 μs |    257.7 μs |    261.0 μs |  0.63 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Roots   | String[2035] |    575.8 μs |  0.87 μs |  0.77 μs |    573.8 μs |    576.0 μs |  1.40 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Wrong**   | **String[4157]** |  **9,260.2 μs** | **35.19 μs** | **32.91 μs** |  **9,201.9 μs** |  **9,268.8 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Wrong   | String[4157] |  6,773.6 μs | 19.55 μs | 18.28 μs |  6,725.6 μs |  6,778.0 μs |  0.73 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Wrong   | String[4157] | 16,964.2 μs | 20.28 μs | 16.94 μs | 16,931.4 μs | 16,967.1 μs |  1.83 |
