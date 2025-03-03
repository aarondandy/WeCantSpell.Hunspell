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
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **512.9 μs** |  **1.66 μs** |  **1.47 μs** |   **510.6 μs** |   **513.0 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   446.6 μs |  1.95 μs |  1.82 μs |   441.0 μs |   446.9 μs |  0.87 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,864.3 μs** | **37.05 μs** | **34.65 μs** | **5,776.1 μs** | **5,865.0 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,690.3 μs | 32.77 μs | 29.05 μs | 5,640.2 μs | 5,698.4 μs |  0.97 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,195.2 μs** | **27.62 μs** | **25.84 μs** | **5,138.3 μs** | **5,196.6 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 4,903.9 μs | 36.95 μs | 28.85 μs | 4,842.7 μs | 4,904.5 μs |  0.94 |
