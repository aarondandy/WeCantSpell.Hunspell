```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.403
  [Host]      : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  Check en-US : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2

Job=Check en-US  Runtime=.NET 8.0  

```
| Method        | set     | words                | Mean       | Error    | StdDev   | Min        | Median     | Ratio |
|-------------- |-------- |--------------------- |-----------:|---------:|---------:|-----------:|-----------:|------:|
| **&#39;Check words&#39;** | **All**     | **Syste(...)ring] [48]** | **5,938.9 μs** | **30.97 μs** | **28.97 μs** | **5,865.7 μs** | **5,941.3 μs** |  **1.00** |
|               |         |                      |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Correct** | **Syste(...)ring] [48]** |   **510.0 μs** |  **8.51 μs** |  **7.10 μs** |   **504.6 μs** |   **507.9 μs** |  **1.00** |
|               |         |                      |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Roots**   | **Syste(...)ring] [48]** |   **250.7 μs** |  **0.44 μs** |  **0.41 μs** |   **249.8 μs** |   **250.8 μs** |  **1.00** |
|               |         |                      |            |          |          |            |            |       |
| **&#39;Check words&#39;** | **Wrong**   | **Syste(...)ring] [48]** | **5,298.3 μs** | **15.97 μs** | **14.15 μs** | **5,278.5 μs** | **5,297.7 μs** |  **1.00** |
