```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-MAJNXI : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-GTHRWI : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-TLSYXV : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2
  Job-YRAWZO : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
| Method     | Runtime            | set     | words        | Mean        | Error     | StdDev    | Min         | Max         | Median      | Ratio | RatioSD |
|----------- |------------------- |-------- |------------- |------------:|----------:|----------:|------------:|------------:|------------:|------:|--------:|
| **CheckWords** | **.NET Framework 4.8** | **All**     | **String[7158]** | **19,136.6 μs** | **346.44 μs** | **206.16 μs** | **18,941.5 μs** | **19,531.2 μs** | **19,024.4 μs** |  **1.86** |    **0.02** |
| CheckWords | .NET 6.0           | All     | String[7158] | 10,375.8 μs | 151.94 μs |  39.46 μs | 10,318.5 μs | 10,410.4 μs | 10,394.8 μs |  1.00 |    0.00 |
| CheckWords | .NET 7.0           | All     | String[7158] |  9,996.3 μs |  97.64 μs |  15.11 μs |  9,979.8 μs | 10,016.0 μs |  9,994.8 μs |  0.96 |    0.01 |
| CheckWords | .NET 8.0           | All     | String[7158] |  7,835.4 μs | 145.43 μs |  22.51 μs |  7,817.7 μs |  7,867.4 μs |  7,828.3 μs |  0.75 |    0.00 |
|            |                    |         |              |             |           |           |             |             |             |       |         |
| **CheckWords** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,387.9 μs** |  **27.53 μs** |  **16.38 μs** |  **1,361.2 μs** |  **1,405.9 μs** |  **1,388.8 μs** |  **1.75** |    **0.02** |
| CheckWords | .NET 6.0           | Correct | String[3001] |    796.5 μs |  13.47 μs |   2.08 μs |    793.9 μs |    798.3 μs |    796.9 μs |  1.00 |    0.00 |
| CheckWords | .NET 7.0           | Correct | String[3001] |    764.5 μs |  14.30 μs |   2.21 μs |    761.4 μs |    766.6 μs |    765.0 μs |  0.96 |    0.00 |
| CheckWords | .NET 8.0           | Correct | String[3001] |    547.9 μs |   8.56 μs |   6.69 μs |    544.2 μs |    568.6 μs |    545.5 μs |  0.69 |    0.01 |
|            |                    |         |              |             |           |           |             |             |             |       |         |
| **CheckWords** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **543.3 μs** |   **3.64 μs** |   **0.20 μs** |    **543.1 μs** |    **543.5 μs** |    **543.4 μs** |  **1.44** |    **0.00** |
| CheckWords | .NET 6.0           | Roots   | String[2035] |    376.2 μs |   5.60 μs |   0.87 μs |    375.2 μs |    377.3 μs |    376.1 μs |  1.00 |    0.00 |
| CheckWords | .NET 7.0           | Roots   | String[2035] |    357.0 μs |   6.70 μs |   1.74 μs |    355.4 μs |    359.3 μs |    356.2 μs |  0.95 |    0.00 |
| CheckWords | .NET 8.0           | Roots   | String[2035] |    243.7 μs |   3.61 μs |   1.29 μs |    242.1 μs |    245.2 μs |    243.7 μs |  0.65 |    0.00 |
|            |                    |         |              |             |           |           |             |             |             |       |         |
| **CheckWords** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **17,101.8 μs** | **145.43 μs** |  **22.51 μs** | **17,077.1 μs** | **17,124.1 μs** | **17,103.1 μs** |  **1.83** |    **0.00** |
| CheckWords | .NET 6.0           | Wrong   | String[4157] |  9,355.9 μs |  84.78 μs |  13.12 μs |  9,337.7 μs |  9,368.6 μs |  9,358.7 μs |  1.00 |    0.00 |
| CheckWords | .NET 7.0           | Wrong   | String[4157] |  9,042.2 μs | 154.60 μs |  23.92 μs |  9,016.7 μs |  9,074.3 μs |  9,039.0 μs |  0.97 |    0.00 |
| CheckWords | .NET 8.0           | Wrong   | String[4157] |  7,395.3 μs | 134.95 μs |  89.26 μs |  7,231.7 μs |  7,527.5 μs |  7,423.1 μs |  0.79 |    0.01 |
