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
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **All**     | **String[7158]** |  **9,964.8 μs** | **46.07 μs** | **38.47 μs** |  **9,911.3 μs** |  **9,960.9 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | All     | String[7158] |  7,399.6 μs | 16.32 μs | 12.74 μs |  7,364.0 μs |  7,400.2 μs |  0.74 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | All     | String[7158] | 18,359.9 μs | 32.90 μs | 29.16 μs | 18,314.7 μs | 18,360.7 μs |  1.84 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Correct** | **String[3001]** |    **808.8 μs** |  **2.82 μs** |  **2.64 μs** |    **805.4 μs** |    **807.3 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Correct | String[3001] |    524.6 μs |  1.45 μs |  1.21 μs |    522.9 μs |    524.8 μs |  0.65 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Correct | String[3001] |  1,389.2 μs |  2.10 μs |  1.96 μs |  1,386.0 μs |  1,389.2 μs |  1.72 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Roots**   | **String[2035]** |    **391.6 μs** |  **3.33 μs** |  **2.78 μs** |    **389.0 μs** |    **390.5 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Roots   | String[2035] |    249.1 μs |  1.18 μs |  1.05 μs |    248.1 μs |    248.8 μs |  0.64 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Roots   | String[2035] |    581.8 μs |  2.51 μs |  2.23 μs |    579.7 μs |    580.4 μs |  1.49 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Wrong**   | **String[4157]** |  **8,881.5 μs** | **12.94 μs** | **12.10 μs** |  **8,866.8 μs** |  **8,883.1 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Wrong   | String[4157] |  6,726.7 μs |  6.33 μs |  4.94 μs |  6,720.1 μs |  6,726.0 μs |  0.76 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Wrong   | String[4157] | 16,735.2 μs | 13.63 μs | 12.08 μs | 16,715.4 μs | 16,732.0 μs |  1.88 |
