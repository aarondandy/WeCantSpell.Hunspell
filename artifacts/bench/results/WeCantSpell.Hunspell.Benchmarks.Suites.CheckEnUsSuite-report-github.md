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
| Method        | Runtime            | set     | words        | Mean        | Error     | StdDev   | Min         | Max         | Median      | Ratio | RatioSD |
|-------------- |------------------- |-------- |------------- |------------:|----------:|---------:|------------:|------------:|------------:|------:|--------:|
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **18,663.9 μs** | **184.01 μs** | **28.48 μs** | **18,630.3 μs** | **18,699.8 μs** | **18,662.7 μs** |  **1.87** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] |  9,971.7 μs |  95.75 μs | 14.82 μs |  9,954.1 μs |  9,988.4 μs |  9,972.1 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  8,251.6 μs |  68.08 μs | 10.54 μs |  8,238.3 μs |  8,260.5 μs |  8,253.8 μs |  0.83 |    0.00 |
|               |                    |         |              |             |           |          |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,362.4 μs** |   **4.86 μs** |  **0.27 μs** |  **1,362.1 μs** |  **1,362.6 μs** |  **1,362.5 μs** |  **1.72** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    790.4 μs |   7.44 μs |  1.15 μs |    789.1 μs |    791.6 μs |    790.5 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    559.8 μs |   9.92 μs |  1.54 μs |    558.3 μs |    561.2 μs |    559.9 μs |  0.71 |    0.00 |
|               |                    |         |              |             |           |          |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **543.6 μs** |   **7.79 μs** |  **2.78 μs** |    **538.9 μs** |    **546.8 μs** |    **544.2 μs** |  **1.46** |    **0.01** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    371.5 μs |   5.84 μs |  2.08 μs |    368.5 μs |    373.8 μs |    372.2 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    240.4 μs |   4.48 μs |  0.69 μs |    239.4 μs |    240.9 μs |    240.7 μs |  0.65 |    0.00 |
|               |                    |         |              |             |           |          |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **16,879.9 μs** | **197.44 μs** | **30.55 μs** | **16,859.7 μs** | **16,924.4 μs** | **16,867.8 μs** |  **1.86** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  9,055.2 μs | 114.91 μs |  6.30 μs |  9,049.1 μs |  9,061.7 μs |  9,054.8 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,435.2 μs | 145.92 μs | 96.52 μs |  7,326.3 μs |  7,625.8 μs |  7,433.6 μs |  0.82 |    0.02 |
