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
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **18,417.0 μs** | **264.01 μs** | **40.86 μs** | **18,358.4 μs** | **18,448.1 μs** | **18,430.7 μs** |  **1.86** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] |  9,915.8 μs | 141.57 μs |  7.76 μs |  9,910.7 μs |  9,924.8 μs |  9,912.1 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  7,782.8 μs | 120.87 μs | 43.10 μs |  7,727.9 μs |  7,830.6 μs |  7,793.4 μs |  0.78 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,359.4 μs** |   **7.60 μs** |  **0.42 μs** |  **1,358.9 μs** |  **1,359.7 μs** |  **1,359.5 μs** |  **1.68** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    808.9 μs |  10.78 μs |  2.80 μs |    805.5 μs |    811.5 μs |    810.0 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    554.3 μs |  10.31 μs |  7.46 μs |    546.0 μs |    570.1 μs |    551.4 μs |  0.69 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **548.3 μs** |   **8.98 μs** |  **0.49 μs** |    **547.7 μs** |    **548.7 μs** |    **548.5 μs** |  **1.47** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    373.7 μs |   0.89 μs |  0.05 μs |    373.6 μs |    373.7 μs |    373.7 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    257.2 μs |   3.29 μs |  0.51 μs |    256.6 μs |    257.6 μs |    257.3 μs |  0.69 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **16,404.3 μs** |  **68.65 μs** |  **3.76 μs** | **16,400.0 μs** | **16,406.8 μs** | **16,406.1 μs** |  **1.83** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  8,952.3 μs |  43.38 μs |  2.38 μs |  8,950.0 μs |  8,954.8 μs |  8,952.0 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  6,877.2 μs | 135.73 μs | 98.14 μs |  6,735.4 μs |  7,021.2 μs |  6,904.8 μs |  0.77 |
