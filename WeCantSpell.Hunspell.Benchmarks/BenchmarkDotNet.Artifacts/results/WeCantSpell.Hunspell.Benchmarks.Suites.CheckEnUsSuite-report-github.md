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
| Method        | Runtime            | set     | words        | Mean        | Error     | StdDev    | Min         | Max         | Median      | Ratio | RatioSD |
|-------------- |------------------- |-------- |------------- |------------:|----------:|----------:|------------:|------------:|------------:|------:|--------:|
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **19,387.8 μs** | **380.11 μs** | **355.56 μs** | **18,864.7 μs** | **20,071.4 μs** | **19,414.1 μs** |  **1.90** |    **0.05** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] | 10,253.2 μs | 176.87 μs | 138.09 μs |  9,988.7 μs | 10,466.6 μs | 10,234.9 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  7,744.8 μs | 105.48 μs |  37.62 μs |  7,693.3 μs |  7,791.1 μs |  7,733.7 μs |  0.75 |    0.01 |
|               |                    |         |              |             |           |           |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,363.9 μs** |  **17.77 μs** |   **0.97 μs** |  **1,362.8 μs** |  **1,364.7 μs** |  **1,364.2 μs** |  **1.72** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    790.9 μs |  10.94 μs |   1.69 μs |    789.1 μs |    792.4 μs |    791.0 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    539.1 μs |   9.80 μs |   7.09 μs |    531.5 μs |    551.5 μs |    536.4 μs |  0.68 |    0.01 |
|               |                    |         |              |             |           |           |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **551.3 μs** |   **5.76 μs** |   **0.89 μs** |    **550.4 μs** |    **552.5 μs** |    **551.2 μs** |  **1.51** |    **0.01** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    365.2 μs |   5.26 μs |   1.87 μs |    363.4 μs |    368.6 μs |    364.9 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    249.1 μs |   3.44 μs |   0.89 μs |    247.9 μs |    250.2 μs |    248.9 μs |  0.68 |    0.00 |
|               |                    |         |              |             |           |           |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **16,820.5 μs** | **297.19 μs** |  **77.18 μs** | **16,706.5 μs** | **16,893.1 μs** | **16,810.9 μs** |  **1.84** |    **0.01** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  9,112.1 μs | 155.29 μs |  55.38 μs |  9,061.1 μs |  9,206.4 μs |  9,091.4 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,440.1 μs | 132.46 μs |  95.78 μs |  7,264.1 μs |  7,594.5 μs |  7,461.4 μs |  0.82 |    0.01 |
