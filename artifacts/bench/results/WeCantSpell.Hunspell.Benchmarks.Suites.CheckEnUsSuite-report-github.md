```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3194)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.200
  [Host]      : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  Check en-US : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2

Job=Check en-US  

```
| Method        | Runtime  | set     | words        | Mean       | Error    | StdDev   | Min        | Median     | Ratio |
|-------------- |--------- |-------- |------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **506.1 μs** |  **1.58 μs** |  **1.40 μs** |   **504.3 μs** |   **505.7 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   450.4 μs |  0.96 μs |  0.85 μs |   449.3 μs |   450.3 μs |  0.89 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,800.9 μs** | **29.00 μs** | **27.13 μs** | **5,742.4 μs** | **5,803.5 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,539.5 μs | 39.31 μs | 34.85 μs | 5,481.1 μs | 5,548.5 μs |  0.95 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,280.9 μs** | **14.50 μs** | **13.57 μs** | **5,259.6 μs** | **5,280.0 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 4,903.8 μs | 51.82 μs | 48.47 μs | 4,814.8 μs | 4,907.3 μs |  0.93 |
