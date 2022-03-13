# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/13/2022 4:18:50 AM_
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
|TotalBytesAllocated |           bytes |   39,862,400.00 |   39,862,400.00 |   39,862,400.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           98.00 |           98.00 |           98.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|    Elapsed Time |              ms |        1,423.00 |        1,423.00 |        1,423.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   28,006,000.76 |   28,006,000.76 |   28,006,000.76 |            0.00 |
|TotalCollections [Gen0] |     collections |           68.85 |           68.85 |           68.85 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.40 |           27.40 |           27.40 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.73 |            7.73 |            7.73 |            0.00 |
|    Elapsed Time |              ms |          999.75 |          999.75 |          999.75 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          124.35 |          124.35 |          124.35 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   39,862,400.00 |   28,006,000.76 |           35.71 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           98.00 |           68.85 |   14,524,001.02 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |           27.40 |   36,496,207.69 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |            7.73 |  129,395,645.45 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,423.00 |          999.75 |    1,000,247.43 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          124.35 |    8,041,537.29 |


