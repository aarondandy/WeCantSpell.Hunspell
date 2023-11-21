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
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **20,161.4 μs** | **229.37 μs** | **35.49 μs** | **20,130.5 μs** | **20,209.5 μs** | **20,152.8 μs** |  **2.01** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] | 10,052.6 μs |  95.40 μs | 14.76 μs | 10,033.5 μs | 10,068.8 μs | 10,054.0 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  7,966.8 μs | 146.42 μs | 22.66 μs |  7,940.4 μs |  7,994.9 μs |  7,965.9 μs |  0.79 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,366.2 μs** |   **3.80 μs** |  **0.59 μs** |  **1,365.5 μs** |  **1,366.9 μs** |  **1,366.2 μs** |  **1.71** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    799.9 μs |  15.06 μs |  0.83 μs |    799.3 μs |    800.8 μs |    799.4 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    572.4 μs |  15.67 μs | 18.04 μs |    552.0 μs |    605.2 μs |    564.0 μs |  0.74 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **546.4 μs** |   **2.15 μs** |  **0.12 μs** |    **546.3 μs** |    **546.5 μs** |    **546.4 μs** |  **1.50** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    363.6 μs |   3.13 μs |  0.17 μs |    363.4 μs |    363.8 μs |    363.7 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    250.8 μs |   4.13 μs |  1.07 μs |    250.0 μs |    252.0 μs |    250.0 μs |  0.69 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **17,914.4 μs** | **299.25 μs** | **16.40 μs** | **17,903.5 μs** | **17,933.3 μs** | **17,906.4 μs** |  **1.86** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  9,609.9 μs |  89.90 μs |  4.93 μs |  9,604.8 μs |  9,614.6 μs |  9,610.3 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,531.0 μs | 138.79 μs | 82.59 μs |  7,442.4 μs |  7,642.6 μs |  7,533.3 μs |  0.78 |
