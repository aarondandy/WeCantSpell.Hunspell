# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/12/2022 3:31:25 AM_
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
|TotalBytesAllocated |           bytes |    4,289,688.00 |    4,289,688.00 |    4,289,688.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.00 |           66.00 |           66.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.00 |            7.00 |            7.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.00 |          118.00 |          118.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,290,761.55 |    4,290,761.55 |    4,290,761.55 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.02 |           66.02 |           66.02 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.01 |           27.01 |           27.01 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.00 |            7.00 |            7.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.25 |        1,000.25 |        1,000.25 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.03 |          118.03 |          118.03 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,289,688.00 |    4,290,761.55 |          233.06 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |           66.02 |   15,147,724.24 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |           27.01 |   37,027,770.37 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            7.00 |            7.00 |  142,821,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,000.00 |        1,000.25 |      999,749.80 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          118.00 |          118.03 |    8,472,455.93 |


