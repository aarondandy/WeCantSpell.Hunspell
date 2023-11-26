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
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **All**     | **String[7158]** | **10,443.3 μs** | **23.04 μs** | **21.55 μs** | **10,396.4 μs** | **10,447.5 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | All     | String[7158] |  7,629.5 μs |  9.30 μs |  8.24 μs |  7,621.3 μs |  7,626.8 μs |  0.73 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | All     | String[7158] | 18,830.7 μs | 60.16 μs | 56.27 μs | 18,631.2 μs | 18,847.4 μs |  1.80 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Correct** | **String[3001]** |    **812.8 μs** |  **1.03 μs** |  **0.96 μs** |    **811.3 μs** |    **813.0 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Correct | String[3001] |    551.5 μs |  0.79 μs |  0.66 μs |    550.4 μs |    551.5 μs |  0.68 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Correct | String[3001] |  1,408.2 μs |  1.07 μs |  1.00 μs |  1,406.7 μs |  1,407.9 μs |  1.73 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Roots**   | **String[2035]** |    **400.4 μs** |  **1.40 μs** |  **1.17 μs** |    **399.1 μs** |    **399.9 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Roots   | String[2035] |    250.6 μs |  1.30 μs |  1.15 μs |    248.1 μs |    250.8 μs |  0.63 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Roots   | String[2035] |    579.3 μs |  0.76 μs |  0.64 μs |    578.1 μs |    579.3 μs |  1.45 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Wrong**   | **String[4157]** |  **9,163.0 μs** | **38.06 μs** | **35.60 μs** |  **9,036.0 μs** |  **9,173.6 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Wrong   | String[4157] |  6,962.6 μs |  8.02 μs |  7.50 μs |  6,949.2 μs |  6,964.1 μs |  0.76 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Wrong   | String[4157] | 16,956.6 μs |  7.68 μs |  6.81 μs | 16,946.4 μs | 16,955.2 μs |  1.85 |
