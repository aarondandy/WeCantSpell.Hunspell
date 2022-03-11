# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/11/2022 2:43:41 AM_
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
NumberOfIterations=1, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   39,858,688.00 |   39,858,688.00 |   39,858,688.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           98.00 |           98.00 |           98.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|    Elapsed Time |              ms |        1,402.00 |        1,402.00 |        1,402.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   28,436,248.09 |   28,436,248.09 |   28,436,248.09 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.92 |           69.92 |           69.92 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.82 |           27.82 |           27.82 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.85 |            7.85 |            7.85 |            0.00 |
|    Elapsed Time |              ms |        1,000.22 |        1,000.22 |        1,000.22 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          126.28 |          126.28 |          126.28 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   39,858,688.00 |   28,436,248.09 |           35.17 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           98.00 |           69.92 |   14,302,917.35 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |           27.82 |   35,940,664.10 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |            7.85 |  127,425,990.91 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,402.00 |        1,000.22 |      999,775.96 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          126.28 |    7,919,129.38 |


