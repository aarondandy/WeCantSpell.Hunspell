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
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **19,915.9 μs** | **349.05 μs** | **19.13 μs** | **19,896.2 μs** | **19,934.4 μs** | **19,917.2 μs** |  **1.98** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] | 10,066.4 μs | 105.16 μs | 27.31 μs | 10,020.2 μs | 10,089.3 μs | 10,070.6 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  8,380.2 μs | 163.75 μs | 25.34 μs |  8,355.7 μs |  8,415.6 μs |  8,374.7 μs |  0.83 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,349.5 μs** |  **11.35 μs** |  **1.76 μs** |  **1,347.2 μs** |  **1,351.5 μs** |  **1,349.7 μs** |  **1.74** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    775.8 μs |   4.71 μs |  0.73 μs |    775.0 μs |    776.8 μs |    775.8 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    551.6 μs |   6.09 μs |  2.70 μs |    548.5 μs |    555.9 μs |    552.0 μs |  0.71 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **540.4 μs** |  **10.25 μs** |  **6.10 μs** |    **533.7 μs** |    **550.2 μs** |    **539.8 μs** |  **1.49** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    363.0 μs |   4.39 μs |  0.24 μs |    362.8 μs |    363.3 μs |    363.1 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    243.5 μs |   4.44 μs |  0.69 μs |    242.9 μs |    244.5 μs |    243.4 μs |  0.67 |
|               |                    |         |              |             |           |          |             |             |             |       |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **18,469.7 μs** | **313.22 μs** | **48.47 μs** | **18,437.1 μs** | **18,540.7 μs** | **18,450.5 μs** |  **2.04** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  9,056.2 μs | 103.06 μs |  5.65 μs |  9,049.7 μs |  9,059.8 μs |  9,059.3 μs |  1.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,233.1 μs | 140.26 μs | 83.47 μs |  7,130.2 μs |  7,376.6 μs |  7,243.5 μs |  0.80 |
