```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-MAJNXI : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-GTHRWI : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-YRAWZO : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
| Method        | Runtime            | set     | words        | Mean        | Error     | StdDev   | Min         | Max         | Median      | Ratio |
|-------------- |------------------- |-------- |------------- |------------:|----------:|---------:|------------:|------------:|------------:|------:|
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **18,985.4 μs** | **205.19 μs** | **31.75 μs** | **18,943.2 μs** | **19,013.0 μs** | **18,992.8 μs** |  **1.89** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] | 10,023.9 μs | 159.67 μs | 24.71 μs | 10,000.7 μs | 10,057.1 μs | 10,018.8 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  7,949.9 μs | 140.55 μs | 21.75 μs |  7,925.0 μs |  7,974.2 μs |  7,950.2 μs |  0.79 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,364.5 μs** |   **2.25 μs** |  **0.12 μs** |  **1,364.4 μs** |  **1,364.6 μs** |  **1,364.4 μs** |  **1.71** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    798.5 μs |  12.34 μs |  1.91 μs |    797.3 μs |    801.4 μs |    797.7 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    575.6 μs |   9.37 μs |  2.43 μs |    572.8 μs |    579.0 μs |    575.2 μs |  0.72 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **541.8 μs** |   **5.15 μs** |  **0.80 μs** |    **540.7 μs** |    **542.4 μs** |    **542.1 μs** |  **1.43** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    378.8 μs |   0.94 μs |  0.05 μs |    378.7 μs |    378.8 μs |    378.8 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    242.4 μs |   4.46 μs |  1.16 μs |    241.5 μs |    244.2 μs |    241.9 μs |  0.64 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **16,793.9 μs** | **211.93 μs** | **32.80 μs** | **16,753.9 μs** | **16,830.6 μs** | **16,795.5 μs** |  **1.86** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  9,029.9 μs | 155.62 μs | 24.08 μs |  9,010.2 μs |  9,062.9 μs |  9,023.3 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,430.4 μs | 147.08 μs | 97.29 μs |  7,314.0 μs |  7,580.1 μs |  7,434.8 μs |  0.82 |
