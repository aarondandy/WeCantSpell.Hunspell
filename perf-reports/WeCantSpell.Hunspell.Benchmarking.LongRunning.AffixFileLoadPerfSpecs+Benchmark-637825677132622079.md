# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/11/2022 3:55:13 AM_
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
|TotalBytesAllocated |           bytes |    4,307,592.00 |    4,307,592.00 |    4,307,592.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.00 |           66.00 |           66.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.00 |            7.00 |            7.00 |            0.00 |
|    Elapsed Time |              ms |          956.00 |          956.00 |          956.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.00 |          118.00 |          118.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,507,053.45 |    4,507,053.45 |    4,507,053.45 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.06 |           69.06 |           69.06 |            0.00 |
|TotalCollections [Gen1] |     collections |           28.25 |           28.25 |           28.25 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.32 |            7.32 |            7.32 |            0.00 |
|    Elapsed Time |              ms |        1,000.27 |        1,000.27 |        1,000.27 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          123.46 |          123.46 |          123.46 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,307,592.00 |    4,507,053.45 |          221.87 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |           69.06 |   14,480,978.79 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |           28.25 |   35,397,948.15 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            7.00 |            7.32 |  136,534,942.86 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          956.00 |        1,000.27 |      999,732.85 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          118.00 |          123.46 |    8,099,530.51 |


