```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]      : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-FYOECP  : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-ACBKDI  : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Check en-US : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256


```
| Method        | Job         | Runtime            | set     | words        | Mean        | Error     | StdDev   | Min         | Median      | Ratio |
|-------------- |------------ |------------------- |-------- |------------- |------------:|----------:|---------:|------------:|------------:|------:|
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **All**     | **String[7158]** | **10,175.5 μs** |  **27.63 μs** | **24.49 μs** | **10,126.6 μs** | **10,179.0 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | All     | String[7158] |  7,595.9 μs |   7.84 μs |  7.33 μs |  7,583.1 μs |  7,596.6 μs |  0.75 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | All     | String[7158] | 18,130.6 μs | 105.50 μs | 82.36 μs | 17,875.6 μs | 18,157.7 μs |  1.78 |
|               |             |                    |         |              |             |           |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Correct** | **String[3001]** |    **820.9 μs** |   **1.93 μs** |  **1.81 μs** |    **818.3 μs** |    **820.3 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Correct | String[3001] |    510.8 μs |   0.92 μs |  0.82 μs |    509.6 μs |    510.8 μs |  0.62 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Correct | String[3001] |  1,336.0 μs |   5.74 μs |  5.37 μs |  1,317.0 μs |  1,337.3 μs |  1.63 |
|               |             |                    |         |              |             |           |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Roots**   | **String[2035]** |    **391.3 μs** |   **0.57 μs** |  **0.51 μs** |    **390.6 μs** |    **391.3 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Roots   | String[2035] |    236.6 μs |   0.42 μs |  0.39 μs |    236.0 μs |    236.6 μs |  0.60 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Roots   | String[2035] |    547.3 μs |   2.35 μs |  2.20 μs |    539.8 μs |    547.8 μs |  1.40 |
|               |             |                    |         |              |             |           |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Wrong**   | **String[4157]** |  **9,199.7 μs** |  **13.80 μs** | **12.23 μs** |  **9,168.4 μs** |  **9,200.9 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Wrong   | String[4157] |  6,967.5 μs |  37.31 μs | 33.07 μs |  6,883.6 μs |  6,975.3 μs |  0.76 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Wrong   | String[4157] | 16,785.5 μs |  11.95 μs | 10.59 μs | 16,768.4 μs | 16,783.4 μs |  1.82 |
