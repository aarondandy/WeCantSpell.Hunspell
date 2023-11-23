```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22631.2715/23H2/2023Update/SunValley3)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
  [Host]     : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-MAJNXI : .NET Framework 4.8.1 (4.8.9181.0), X64 RyuJIT VectorSize=256
  Job-GTHRWI : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  Job-YRAWZO : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

MinInvokeCount=1  IterationTime=250.0000 ms  MaxIterationCount=20  
MaxWarmupIterationCount=5  MinIterationCount=1  MinWarmupIterationCount=1  

```
| Method          | Runtime            | Mean     | Error    | StdDev  | Ratio |
|---------------- |------------------- |---------:|---------:|--------:|------:|
| &#39;Suggest words&#39; | .NET Framework 4.8 | 820.7 ms | 16.27 ms | 2.52 ms |  2.00 |
| &#39;Suggest words&#39; | .NET 6.0           | 410.4 ms |  7.56 ms | 1.17 ms |  1.00 |
| &#39;Suggest words&#39; | .NET 8.0           | 376.9 ms |  5.85 ms | 0.91 ms |  0.92 |
