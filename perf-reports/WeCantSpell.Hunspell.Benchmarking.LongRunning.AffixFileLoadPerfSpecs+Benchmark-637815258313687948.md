# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/27/2022 2:30:31 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   13,585,176.00 |   13,585,176.00 |   13,585,176.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          134.00 |          134.00 |          134.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           35.00 |           35.00 |           35.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|    Elapsed Time |              ms |        1,954.00 |        1,954.00 |        1,954.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,953,258.69 |    6,953,258.69 |    6,953,258.69 |            0.00 |
|TotalCollections [Gen0] |     collections |           68.58 |           68.58 |           68.58 |            0.00 |
|TotalCollections [Gen1] |     collections |           17.91 |           17.91 |           17.91 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.12 |            5.12 |            5.12 |            0.00 |
|    Elapsed Time |              ms |        1,000.11 |        1,000.11 |        1,000.11 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           30.20 |           30.20 |           30.20 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,585,176.00 |    6,953,258.69 |          143.82 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          134.00 |           68.58 |   14,580,488.81 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           35.00 |           17.91 |   55,822,442.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |            5.12 |  195,378,550.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,954.00 |        1,000.11 |      999,890.23 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           30.20 |   33,115,008.47 |


