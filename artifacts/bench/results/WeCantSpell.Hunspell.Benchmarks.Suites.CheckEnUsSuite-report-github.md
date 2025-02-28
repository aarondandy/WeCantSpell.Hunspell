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
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **501.4 μs** |  **1.12 μs** |  **0.87 μs** |   **498.9 μs** |   **501.6 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   455.1 μs |  1.23 μs |  1.15 μs |   452.4 μs |   455.3 μs |  0.91 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,793.8 μs** | **20.15 μs** | **15.73 μs** | **5,751.9 μs** | **5,797.3 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,619.4 μs | 20.30 μs | 18.00 μs | 5,591.8 μs | 5,615.7 μs |  0.97 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,198.8 μs** | **19.52 μs** | **18.26 μs** | **5,177.5 μs** | **5,193.4 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 4,908.9 μs | 27.08 μs | 24.00 μs | 4,867.6 μs | 4,909.6 μs |  0.94 |
