# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/12/2022 4:17:01 AM_
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
|TotalBytesAllocated |           bytes |   93,856,808.00 |   93,856,808.00 |   93,856,808.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          330.00 |          330.00 |          330.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          166.00 |          166.00 |          166.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|    Elapsed Time |              ms |       11,417.00 |       11,417.00 |       11,417.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,220,907.23 |    8,220,907.23 |    8,220,907.23 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.90 |           28.90 |           28.90 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.54 |           14.54 |           14.54 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.28 |            2.28 |            2.28 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.17 |            5.17 |            5.17 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   93,856,808.00 |    8,220,907.23 |          121.64 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          330.00 |           28.90 |   34,596,494.24 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          166.00 |           14.54 |   68,776,163.25 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |            2.28 |  439,109,350.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,417.00 |        1,000.01 |      999,986.26 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.17 |  193,505,815.25 |


