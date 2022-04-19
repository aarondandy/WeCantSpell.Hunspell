# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/19/2022 12:39:40 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |  166,436,112.00 |  166,436,112.00 |  166,436,112.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          265.00 |          265.00 |          265.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          139.00 |          139.00 |          139.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|    Elapsed Time |              ms |       11,752.00 |       11,752.00 |       11,752.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   14,163,108.46 |   14,163,108.46 |   14,163,108.46 |            0.00 |
|TotalCollections [Gen0] |     collections |           22.55 |           22.55 |           22.55 |            0.00 |
|TotalCollections [Gen1] |     collections |           11.83 |           11.83 |           11.83 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.21 |            2.21 |            2.21 |            0.00 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.05 |        1,000.05 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.02 |            5.02 |            5.02 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  166,436,112.00 |   14,163,108.46 |           70.61 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          265.00 |           22.55 |   44,344,841.51 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          139.00 |           11.83 |   84,542,323.74 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |            2.21 |  451,976,269.23 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,752.00 |        1,000.05 |      999,947.50 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.02 |  199,175,983.05 |


