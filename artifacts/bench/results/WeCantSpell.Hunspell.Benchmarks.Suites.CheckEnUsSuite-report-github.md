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
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **19,305.9 μs** | **372.80 μs** | **20.43 μs** | **19,287.2 μs** | **19,327.7 μs** | **19,302.7 μs** |  **1.94** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] |  9,934.9 μs |  69.51 μs | 10.76 μs |  9,922.9 μs |  9,948.4 μs |  9,934.2 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  8,040.2 μs | 103.16 μs | 36.79 μs |  7,999.4 μs |  8,086.6 μs |  8,034.1 μs |  0.81 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,409.2 μs** |  **11.77 μs** |  **1.82 μs** |  **1,406.6 μs** |  **1,410.7 μs** |  **1,409.8 μs** |  **1.70** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    828.6 μs |   8.28 μs |  0.45 μs |    828.1 μs |    829.0 μs |    828.7 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    538.5 μs |   4.41 μs |  0.68 μs |    537.9 μs |    539.4 μs |    538.3 μs |  0.65 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **581.1 μs** |   **9.86 μs** |  **0.54 μs** |    **580.5 μs** |    **581.5 μs** |    **581.3 μs** |  **1.48** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    391.7 μs |   4.35 μs |  0.67 μs |    390.9 μs |    392.5 μs |    391.6 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    262.7 μs |   3.60 μs |  0.93 μs |    261.9 μs |    264.1 μs |    262.5 μs |  0.67 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **17,465.3 μs** | **140.17 μs** | **21.69 μs** | **17,435.9 μs** | **17,487.8 μs** | **17,468.8 μs** |  **1.95** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  8,957.8 μs |  31.62 μs |  1.73 μs |  8,956.0 μs |  8,959.5 μs |  8,957.8 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,389.3 μs | 144.12 μs | 85.76 μs |  7,285.8 μs |  7,509.5 μs |  7,348.6 μs |  0.83 |
