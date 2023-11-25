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
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **18,823.6 μs** | **323.49 μs** | **84.01 μs** | **18,725.3 μs** | **18,914.7 μs** | **18,864.2 μs** |  **1.92** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] |  9,811.7 μs | 187.63 μs | 48.73 μs |  9,725.0 μs |  9,841.1 μs |  9,830.4 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  8,041.8 μs | 146.56 μs | 76.65 μs |  7,962.9 μs |  8,174.5 μs |  8,033.0 μs |  0.82 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,411.3 μs** |   **7.91 μs** |  **0.43 μs** |  **1,410.8 μs** |  **1,411.7 μs** |  **1,411.5 μs** |  **1.70** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    832.6 μs |  12.72 μs |  1.97 μs |    830.0 μs |    834.1 μs |    833.1 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    534.4 μs |   3.25 μs |  2.71 μs |    531.1 μs |    541.4 μs |    534.2 μs |  0.64 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **581.3 μs** |   **7.35 μs** |  **1.91 μs** |    **579.4 μs** |    **584.5 μs** |    **580.8 μs** |  **1.50** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    387.3 μs |   7.00 μs |  2.50 μs |    384.6 μs |    390.7 μs |    386.8 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    255.1 μs |   4.15 μs |  1.84 μs |    253.1 μs |    258.2 μs |    254.7 μs |  0.66 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **17,276.2 μs** | **193.11 μs** | **29.88 μs** | **17,247.7 μs** | **17,315.1 μs** | **17,271.0 μs** |  **1.88** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  9,168.6 μs |  60.87 μs |  9.42 μs |  9,158.9 μs |  9,181.2 μs |  9,167.2 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,365.8 μs | 138.74 μs | 91.77 μs |  7,227.3 μs |  7,440.7 μs |  7,407.4 μs |  0.81 |
