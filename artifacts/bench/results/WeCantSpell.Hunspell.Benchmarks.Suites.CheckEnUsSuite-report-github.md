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
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **503.5 μs** |  **1.51 μs** |  **1.41 μs** |   **501.8 μs** |   **503.7 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   450.7 μs |  0.97 μs |  0.86 μs |   449.4 μs |   450.4 μs |  0.90 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,940.9 μs** | **17.98 μs** | **16.82 μs** | **5,915.7 μs** | **5,936.8 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,804.4 μs | 20.99 μs | 18.61 μs | 5,774.3 μs | 5,803.3 μs |  0.98 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,134.3 μs** | **19.98 μs** | **18.69 μs** | **5,103.0 μs** | **5,134.0 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 5,019.3 μs | 34.37 μs | 30.47 μs | 4,944.3 μs | 5,028.0 μs |  0.98 |
