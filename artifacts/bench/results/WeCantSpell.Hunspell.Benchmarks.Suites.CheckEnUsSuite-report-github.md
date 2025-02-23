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
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **511.9 μs** |  **1.76 μs** |  **1.65 μs** |   **507.2 μs** |   **512.4 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   445.6 μs |  1.79 μs |  1.67 μs |   441.7 μs |   445.9 μs |  0.87 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,841.4 μs** | **19.60 μs** | **17.38 μs** | **5,817.8 μs** | **5,841.1 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,577.7 μs | 24.41 μs | 22.83 μs | 5,527.3 μs | 5,578.9 μs |  0.95 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,183.9 μs** | **31.17 μs** | **29.15 μs** | **5,121.5 μs** | **5,178.8 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 4,986.9 μs | 17.01 μs | 15.91 μs | 4,946.1 μs | 4,992.7 μs |  0.96 |
