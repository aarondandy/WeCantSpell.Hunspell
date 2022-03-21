# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/21/2022 4:21:59 AM_
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
|TotalBytesAllocated |           bytes |   40,472,912.00 |   40,472,912.00 |   40,472,912.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           99.00 |           99.00 |           99.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           38.00 |           38.00 |           38.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|    Elapsed Time |              ms |        1,466.00 |        1,466.00 |        1,466.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   27,615,499.81 |   27,615,499.81 |   27,615,499.81 |            0.00 |
|TotalCollections [Gen0] |     collections |           67.55 |           67.55 |           67.55 |            0.00 |
|TotalCollections [Gen1] |     collections |           25.93 |           25.93 |           25.93 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.19 |            8.19 |            8.19 |            0.00 |
|    Elapsed Time |              ms |        1,000.28 |        1,000.28 |        1,000.28 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          120.77 |          120.77 |          120.77 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   40,472,912.00 |   27,615,499.81 |           36.21 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           99.00 |           67.55 |   14,803,907.07 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           38.00 |           25.93 |   38,568,073.68 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |            8.19 |  122,132,233.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,466.00 |        1,000.28 |      999,718.14 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          120.77 |    8,280,151.41 |


