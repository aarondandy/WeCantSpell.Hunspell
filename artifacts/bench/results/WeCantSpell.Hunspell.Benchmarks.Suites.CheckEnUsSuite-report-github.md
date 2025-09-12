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
| **&#39;Check words&#39;** | **Correct** | **String[3000]** |   **457.7 μs** |  **2.24 μs** |  **1.98 μs** |   **452.0 μs** |   **457.8 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Mix**     | **String[7000]** | **5,541.8 μs** | **14.93 μs** | **13.97 μs** | **5,509.3 μs** | **5,544.9 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Wrong**   | **String[4000]** | **4,906.6 μs** | **16.65 μs** | **15.58 μs** | **4,883.4 μs** | **4,909.3 μs** |  **1.00** |
