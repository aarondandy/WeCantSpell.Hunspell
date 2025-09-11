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
| **&#39;Check words&#39;** | **Correct** | **String[3000]** |   **474.7 μs** |  **2.48 μs** |  **2.32 μs** |   **470.3 μs** |   **475.0 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Mix**     | **String[7000]** | **5,622.4 μs** | **15.44 μs** | **14.44 μs** | **5,601.9 μs** | **5,623.2 μs** |  **1.00** |
|               |         |              |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Wrong**   | **String[4000]** | **5,065.1 μs** | **23.90 μs** | **22.36 μs** | **5,001.1 μs** | **5,063.7 μs** |  **1.00** |
