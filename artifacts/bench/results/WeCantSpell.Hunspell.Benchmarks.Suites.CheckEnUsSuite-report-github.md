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
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **516.2 μs** |  **1.06 μs** |  **0.99 μs** |   **514.1 μs** |   **516.2 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   450.9 μs |  1.10 μs |  1.03 μs |   448.7 μs |   451.0 μs |  0.87 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,959.7 μs** | **21.71 μs** | **20.31 μs** | **5,926.2 μs** | **5,958.1 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,675.0 μs | 26.30 μs | 24.60 μs | 5,645.8 μs | 5,667.4 μs |  0.95 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,174.1 μs** | **26.41 μs** | **24.70 μs** | **5,137.2 μs** | **5,179.1 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 4,841.7 μs | 14.16 μs | 11.83 μs | 4,811.9 μs | 4,843.0 μs |  0.94 |
