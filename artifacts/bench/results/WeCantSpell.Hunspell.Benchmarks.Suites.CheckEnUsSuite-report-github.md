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
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **All**     | **String[7158]** |  **9,969.5 μs** | **26.23 μs** | **21.91 μs** |  **9,944.2 μs** |  **9,969.5 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | All     | String[7158] |  7,672.5 μs | 58.77 μs | 54.97 μs |  7,611.1 μs |  7,667.4 μs |  0.77 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | All     | String[7158] | 19,128.6 μs | 20.07 μs | 17.79 μs | 19,090.7 μs | 19,130.1 μs |  1.92 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Correct** | **String[3001]** |    **817.5 μs** |  **5.94 μs** |  **5.56 μs** |    **803.6 μs** |    **819.3 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Correct | String[3001] |    534.8 μs |  0.48 μs |  0.43 μs |    534.1 μs |    534.8 μs |  0.65 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Correct | String[3001] |  1,424.6 μs |  1.12 μs |  1.04 μs |  1,422.9 μs |  1,424.9 μs |  1.74 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Roots**   | **String[2035]** |    **396.0 μs** |  **1.44 μs** |  **1.28 μs** |    **394.0 μs** |    **395.8 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Roots   | String[2035] |    249.9 μs |  0.64 μs |  0.60 μs |    249.0 μs |    250.2 μs |  0.63 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Roots   | String[2035] |    590.6 μs |  1.97 μs |  1.74 μs |    584.9 μs |    591.0 μs |  1.49 |
|               |             |                    |         |              |             |          |          |             |             |       |
| **&#39;Check words&#39;** | **Job-FYOECP**  | **.NET 6.0**           | **Wrong**   | **String[4157]** |  **8,901.4 μs** |  **6.56 μs** |  **5.48 μs** |  **8,892.1 μs** |  **8,901.2 μs** |  **1.00** |
| &#39;Check words&#39; | Job-ACBKDI  | .NET 8.0           | Wrong   | String[4157] |  6,904.6 μs |  8.59 μs |  8.03 μs |  6,891.7 μs |  6,908.7 μs |  0.78 |
| &#39;Check words&#39; | Check en-US | .NET Framework 4.8 | Wrong   | String[4157] | 17,217.0 μs | 15.93 μs | 14.90 μs | 17,199.0 μs | 17,211.5 μs |  1.93 |
