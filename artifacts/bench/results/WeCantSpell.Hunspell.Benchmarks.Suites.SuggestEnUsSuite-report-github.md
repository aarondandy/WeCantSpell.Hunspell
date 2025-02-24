```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3194)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.200
  [Host]        : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2
  Suggest en-US : .NET 8.0.13 (8.0.1325.6609), X64 RyuJIT AVX2

Job=Suggest en-US  

```
| Method          | Runtime  | Mean    | Error    | StdDev   | Min     | Median  | Ratio |
|---------------- |--------- |--------:|---------:|---------:|--------:|--------:|------:|
| &#39;Suggest words&#39; | .NET 8.0 | 1.570 s | 0.0059 s | 0.0052 s | 1.555 s | 1.572 s |  1.00 |
| &#39;Suggest words&#39; | .NET 9.0 | 1.501 s | 0.0030 s | 0.0027 s | 1.497 s | 1.502 s |  0.96 |
