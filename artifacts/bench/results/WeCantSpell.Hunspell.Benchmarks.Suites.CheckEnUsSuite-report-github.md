```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.305
  [Host]      : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Check en-US : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=Check en-US  Runtime=.NET 9.0  

```
| Method        | set     | words        | Mean       | Error    | StdDev   | Min        | Median     | Ratio |
|-------------- |-------- |------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|
| **&#39;Check words&#39;** | **Correct** | **String[3000]** |   **469.2 μs** |  **2.43 μs** |  **2.15 μs** |   **465.2 μs** |   **469.3 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Mix**     | **String[7000]** | **5,632.9 μs** | **46.27 μs** | **43.28 μs** | **5,578.2 μs** | **5,631.4 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Wrong**   | **String[4000]** | **4,953.4 μs** | **25.87 μs** | **24.20 μs** | **4,909.2 μs** | **4,959.7 μs** |  **1.00** |
