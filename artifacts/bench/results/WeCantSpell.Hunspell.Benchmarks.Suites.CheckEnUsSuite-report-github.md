```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.403
  [Host]      : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2
  Check en-US : .NET 8.0.10 (8.0.1024.46610), X64 RyuJIT AVX2

Job=Check en-US  Runtime=.NET 8.0  

```
| Method        | set     | words                | Mean       | Error     | StdDev    | Min        | Median     | Ratio |
|-------------- |-------- |--------------------- |-----------:|----------:|----------:|-----------:|-----------:|------:|
| **&#39;Check words&#39;** | **All**     | **Syste(...)ring] [48]** | **5,979.8 μs** | **114.30 μs** | **152.58 μs** | **5,796.3 μs** | **5,900.3 μs** |  **1.00** |
|               |         |                      |            |           |           |            |            |       |
| **&#39;Check words&#39;** | **Correct** | **Syste(...)ring] [48]** |   **507.8 μs** |   **2.22 μs** |   **1.86 μs** |   **505.5 μs** |   **507.3 μs** |  **1.00** |
|               |         |                      |            |           |           |            |            |       |
| **&#39;Check words&#39;** | **Roots**   | **Syste(...)ring] [48]** |   **257.9 μs** |   **3.21 μs** |   **2.84 μs** |   **253.4 μs** |   **258.1 μs** |  **1.00** |
|               |         |                      |            |           |           |            |            |       |
| **&#39;Check words&#39;** | **Wrong**   | **Syste(...)ring] [48]** | **5,405.6 μs** |  **65.42 μs** |  **61.19 μs** | **5,310.4 μs** | **5,396.3 μs** |  **1.00** |
