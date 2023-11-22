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
| Method        | Runtime            | set     | words        | Mean        | Error     | StdDev   | Min         | Max         | Median      | Ratio | RatioSD |
|-------------- |------------------- |-------- |------------- |------------:|----------:|---------:|------------:|------------:|------------:|------:|--------:|
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **All**     | **String[7158]** | **18,700.1 μs** | **210.07 μs** | **11.51 μs** | **18,687.0 μs** | **18,708.4 μs** | **18,704.9 μs** |  **1.88** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | All     | String[7158] |  9,968.4 μs |  65.45 μs | 10.13 μs |  9,955.6 μs |  9,977.6 μs |  9,970.2 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 7.0           | All     | String[7158] |  9,916.0 μs |  82.26 μs | 12.73 μs |  9,902.9 μs |  9,930.9 μs |  9,915.1 μs |  0.99 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | All     | String[7158] |  7,867.9 μs | 106.75 μs | 27.72 μs |  7,850.2 μs |  7,916.1 μs |  7,855.9 μs |  0.79 |    0.00 |
|               |                    |         |              |             |           |          |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Correct** | **String[3001]** |  **1,360.6 μs** |  **19.22 μs** |  **2.97 μs** |  **1,357.9 μs** |  **1,364.1 μs** |  **1,360.2 μs** |  **1.73** |    **0.01** |
| &#39;Check words&#39; | .NET 6.0           | Correct | String[3001] |    786.7 μs |  13.87 μs |  2.15 μs |    784.2 μs |    789.0 μs |    786.8 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 7.0           | Correct | String[3001] |    753.5 μs |   8.34 μs |  1.29 μs |    752.2 μs |    754.9 μs |    753.5 μs |  0.96 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Correct | String[3001] |    567.7 μs |  10.47 μs |  1.62 μs |    566.1 μs |    569.8 μs |    567.4 μs |  0.72 |    0.00 |
|               |                    |         |              |             |           |          |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Roots**   | **String[2035]** |    **552.2 μs** |   **6.33 μs** |  **1.64 μs** |    **550.5 μs** |    **554.6 μs** |    **552.0 μs** |  **1.49** |    **0.02** |
| &#39;Check words&#39; | .NET 6.0           | Roots   | String[2035] |    368.8 μs |   6.70 μs |  4.84 μs |    362.6 μs |    376.2 μs |    368.0 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 7.0           | Roots   | String[2035] |    359.5 μs |   0.93 μs |  0.14 μs |    359.4 μs |    359.7 μs |    359.5 μs |  0.97 |    0.01 |
| &#39;Check words&#39; | .NET 8.0           | Roots   | String[2035] |    247.8 μs |   3.69 μs |  1.32 μs |    246.6 μs |    249.6 μs |    247.3 μs |  0.67 |    0.01 |
|               |                    |         |              |             |           |          |             |             |             |       |         |
| **&#39;Check words&#39;** | **.NET Framework 4.8** | **Wrong**   | **String[4157]** | **16,846.0 μs** | **335.78 μs** | **18.41 μs** | **16,830.7 μs** | **16,866.4 μs** | **16,840.8 μs** |  **1.88** |    **0.00** |
| &#39;Check words&#39; | .NET 6.0           | Wrong   | String[4157] |  8,938.6 μs |  22.06 μs |  1.21 μs |  8,937.8 μs |  8,940.0 μs |  8,938.2 μs |  1.00 |    0.00 |
| &#39;Check words&#39; | .NET 7.0           | Wrong   | String[4157] |  9,049.0 μs | 102.90 μs | 15.92 μs |  9,034.8 μs |  9,071.2 μs |  9,044.9 μs |  1.01 |    0.00 |
| &#39;Check words&#39; | .NET 8.0           | Wrong   | String[4157] |  7,160.8 μs | 136.99 μs | 99.05 μs |  7,011.0 μs |  7,353.6 μs |  7,159.6 μs |  0.80 |    0.02 |
