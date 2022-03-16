# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/17/2022 12:52:52 AM_
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
|TotalBytesAllocated |           bytes |   95,445,920.00 |   95,445,920.00 |   95,445,920.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          325.00 |          325.00 |          325.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          161.00 |          161.00 |          161.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           23.00 |           23.00 |           23.00 |            0.00 |
|    Elapsed Time |              ms |       11,340.00 |       11,340.00 |       11,340.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,417,126.57 |    8,417,126.57 |    8,417,126.57 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.66 |           28.66 |           28.66 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.20 |           14.20 |           14.20 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.03 |            2.03 |            2.03 |            0.00 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.05 |        1,000.05 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.20 |            5.20 |            5.20 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   95,445,920.00 |    8,417,126.57 |          118.81 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          325.00 |           28.66 |   34,890,737.54 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          161.00 |           14.20 |   70,431,613.04 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           23.00 |            2.03 |  493,021,291.30 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,340.00 |        1,000.05 |      999,955.00 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.20 |  192,194,740.68 |


