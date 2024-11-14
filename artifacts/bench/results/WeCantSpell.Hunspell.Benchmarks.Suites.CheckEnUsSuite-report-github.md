```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]      : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  Check en-US : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2

Job=Check en-US  

```
| Method        | Runtime  | set     | words        | Mean       | Error    | StdDev   | Min        | Median     | Ratio |
|-------------- |--------- |-------- |------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|
| **&#39;Check words&#39;** | **.NET 8.0** | **Correct** | **String[3000]** |   **526.1 μs** |  **1.23 μs** |  **1.15 μs** |   **524.2 μs** |   **526.0 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Correct | String[3000] |   467.2 μs |  0.82 μs |  0.68 μs |   465.9 μs |   467.4 μs |  0.89 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Mix**     | **String[7000]** | **5,850.2 μs** | **28.16 μs** | **26.34 μs** | **5,777.8 μs** | **5,852.9 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Mix     | String[7000] | 5,721.1 μs | 42.13 μs | 37.35 μs | 5,683.4 μs | 5,700.6 μs |  0.98 |
|               |          |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **.NET 8.0** | **Wrong**   | **String[4000]** | **5,070.4 μs** | **28.20 μs** | **24.99 μs** | **5,047.2 μs** | **5,059.7 μs** |  **1.00** |
| &#39;Check words&#39; | .NET 9.0 | Wrong   | String[4000] | 4,713.5 μs | 32.12 μs | 25.08 μs | 4,680.4 μs | 4,713.8 μs |  0.93 |
