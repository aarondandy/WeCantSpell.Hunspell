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
| **&#39;Check words&#39;** | **Correct** | **String[3000]** |   **464.1 μs** |  **0.93 μs** |  **0.87 μs** |   **462.8 μs** |   **463.8 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Mix**     | **String[7000]** | **5,532.1 μs** | **15.83 μs** | **14.03 μs** | **5,503.9 μs** | **5,534.7 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Wrong**   | **String[4000]** | **4,952.4 μs** | **17.83 μs** | **16.68 μs** | **4,907.3 μs** | **4,953.0 μs** |  **1.00** |
