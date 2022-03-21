# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/21/2022 3:18:14 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,869,952.00 |    7,869,898.67 |    7,869,872.00 |           46.19 |
|TotalCollections [Gen0] |     collections |           43.00 |           43.00 |           43.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          636.00 |          633.33 |          632.00 |            2.31 |
|[Counter] WordsChecked |      operations |      613,312.00 |      613,312.00 |      613,312.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   12,451,503.61 |   12,422,046.94 |   12,373,774.54 |       42,142.32 |
|TotalCollections [Gen0] |     collections |           68.03 |           67.87 |           67.61 |            0.23 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.97 |          999.66 |          999.08 |            0.50 |
|[Counter] WordsChecked |      operations |      970,366.05 |      968,067.17 |      964,298.69 |        3,289.83 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,869,872.00 |   12,440,862.65 |           80.38 |
|               2 |    7,869,952.00 |   12,373,774.54 |           80.82 |
|               3 |    7,869,872.00 |   12,451,503.61 |           80.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           43.00 |           67.98 |   14,711,220.93 |
|               2 |           43.00 |           67.61 |   14,791,132.56 |
|               3 |           43.00 |           68.03 |   14,698,648.84 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  632,582,500.00 |
|               2 |            0.00 |            0.00 |  636,018,700.00 |
|               3 |            0.00 |            0.00 |  632,041,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  632,582,500.00 |
|               2 |            0.00 |            0.00 |  636,018,700.00 |
|               3 |            0.00 |            0.00 |  632,041,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          632.00 |          999.08 |    1,000,921.68 |
|               2 |          636.00 |          999.97 |    1,000,029.40 |
|               3 |          632.00 |          999.93 |    1,000,066.30 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      613,312.00 |      969,536.78 |        1,031.42 |
|               2 |      613,312.00 |      964,298.69 |        1,037.02 |
|               3 |      613,312.00 |      970,366.05 |        1,030.54 |


