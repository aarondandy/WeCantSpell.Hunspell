# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/9/2022 2:42:40 AM_
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
|TotalBytesAllocated |           bytes |  117,517,528.00 |  117,517,528.00 |  117,517,528.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          365.00 |          365.00 |          365.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          184.00 |          184.00 |          184.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           28.00 |           28.00 |           28.00 |            0.00 |
|    Elapsed Time |              ms |       12,514.00 |       12,514.00 |       12,514.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,391,056.23 |    9,391,056.23 |    9,391,056.23 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.17 |           29.17 |           29.17 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.70 |           14.70 |           14.70 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.24 |            2.24 |            2.24 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.02 |        1,000.02 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.71 |            4.71 |            4.71 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  117,517,528.00 |    9,391,056.23 |          106.48 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          365.00 |           29.17 |   34,284,304.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          184.00 |           14.70 |   68,009,625.54 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           28.00 |            2.24 |  446,920,396.43 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       12,514.00 |        1,000.02 |      999,981.71 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.71 |  212,097,815.25 |


