```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3194)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.200
  [Host]      : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  Check en-US : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2

Job=Check en-US  

```
| Method        | Runtime  | set     | words        | Mean       | Error    | StdDev   | Median     | Min        | Ratio | RatioSD |
|-------------- |--------- |-------- |------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|--------:|
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **515.2 μs** |  **4.63 μs** |  **4.11 μs** |   **514.2 μs** |   **510.4 μs** |  **1.00** |    **0.01** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   455.4 μs |  8.92 μs | 12.50 μs |   448.2 μs |   444.4 μs |  0.88 |    0.02 |
|               |          |         |              |            |          |          |            |            |       |         |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,837.6 μs** | **20.85 μs** | **19.51 μs** | **5,836.2 μs** | **5,808.1 μs** |  **1.00** |    **0.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,554.0 μs | 25.72 μs | 22.80 μs | 5,559.3 μs | 5,490.8 μs |  0.95 |    0.00 |
|               |          |         |              |            |          |          |            |            |       |         |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,159.4 μs** | **27.08 μs** | **25.33 μs** | **5,163.6 μs** | **5,103.9 μs** |  **1.00** |    **0.01** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 4,962.5 μs | 37.89 μs | 33.59 μs | 4,969.0 μs | 4,869.5 μs |  0.96 |    0.01 |
