```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.403
  [Host]      : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  Check en-US : .NET 6.0.35 (6.0.3524.45918), X64 RyuJIT AVX2

Job=Check en-US  

```
| Method        | Runtime  | set     | words                | Mean       | Error    | StdDev   | Min        | Median     | Ratio |
|-------------- |--------- |-------- |--------------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|
| **&#39;Check words&#39;** | **.NET 6.0** | **All**     | **Syste(...)ring] [48]** | **7,924.5 μs** | **17.87 μs** | **16.72 μs** | **7,899.6 μs** | **7,925.7 μs** |  **1.35** |
| &#39;Check words&#39; | .NET 8.0 | All     | Syste(...)ring] [48] | 5,878.5 μs | 42.04 μs | 39.33 μs | 5,824.8 μs | 5,871.1 μs |  1.00 |
|               |          |         |                      |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 6.0** | **Correct** | **Syste(...)ring] [48]** |   **695.8 μs** |  **1.38 μs** |  **1.29 μs** |   **694.1 μs** |   **695.9 μs** |  **1.36** |
| &#39;Check words&#39; | .NET 8.0 | Correct | Syste(...)ring] [48] |   512.4 μs |  0.98 μs |  0.91 μs |   511.2 μs |   512.3 μs |  1.00 |
|               |          |         |                      |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 6.0** | **Roots**   | **Syste(...)ring] [48]** |   **303.0 μs** |  **1.07 μs** |  **1.00 μs** |   **300.2 μs** |   **303.1 μs** |  **1.21** |
| &#39;Check words&#39; | .NET 8.0 | Roots   | Syste(...)ring] [48] |   249.4 μs |  1.15 μs |  1.08 μs |   245.9 μs |   249.8 μs |  1.00 |
|               |          |         |                      |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 6.0** | **Wrong**   | **Syste(...)ring] [48]** | **6,898.0 μs** | **16.06 μs** | **14.23 μs** | **6,858.8 μs** | **6,900.3 μs** |  **1.30** |
| &#39;Check words&#39; | .NET 8.0 | Wrong   | Syste(...)ring] [48] | 5,311.5 μs | 40.54 μs | 37.92 μs | 5,262.1 μs | 5,307.1 μs |  1.00 |
