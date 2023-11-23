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
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **18,611.7 μs** | **222.26 μs** |  **12.18 μs** | **18,597.8 μs** | **18,620.4 μs** | **18,616.8 μs** |  **1.87** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] |  9,930.7 μs |  84.23 μs |  13.03 μs |  9,912.3 μs |  9,943.1 μs |  9,933.8 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  7,902.9 μs | 121.80 μs |  43.44 μs |  7,869.7 μs |  7,987.4 μs |  7,889.1 μs |  0.80 |    0.01 |
|               |                    |         |              |             |           |           |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,363.2 μs** |  **21.76 μs** |   **1.19 μs** |  **1,362.3 μs** |  **1,364.5 μs** |  **1,362.7 μs** |  **1.74** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    784.4 μs |   8.98 μs |   0.49 μs |    783.9 μs |    784.8 μs |    784.6 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    573.5 μs |   7.50 μs |   4.47 μs |    567.4 μs |    581.3 μs |    572.8 μs |  0.74 |    0.01 |
|               |                    |         |              |             |           |           |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **562.1 μs** |  **13.73 μs** |  **15.26 μs** |    **551.9 μs** |    **592.0 μs** |    **553.8 μs** |  **1.59** |    **0.04** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    366.0 μs |   5.29 μs |   1.37 μs |    363.9 μs |    367.2 μs |    366.3 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    241.7 μs |   4.35 μs |   0.67 μs |    241.1 μs |    242.6 μs |    241.6 μs |  0.66 |    0.00 |
|               |                    |         |              |             |           |           |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **17,707.5 μs** | **480.63 μs** | **553.49 μs** | **16,743.1 μs** | **18,581.6 μs** | **17,843.7 μs** |  **1.98** |    **0.03** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  9,038.7 μs |  79.60 μs |  35.34 μs |  8,985.4 μs |  9,091.9 μs |  9,042.3 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,360.2 μs | 129.96 μs |  93.97 μs |  7,218.5 μs |  7,479.9 μs |  7,372.6 μs |  0.81 |    0.01 |
