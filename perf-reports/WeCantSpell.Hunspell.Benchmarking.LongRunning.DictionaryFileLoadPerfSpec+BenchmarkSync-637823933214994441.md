# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/9/2022 3:28:41 AM_
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
|TotalBytesAllocated |           bytes |  107,383,976.00 |  107,383,976.00 |  107,383,976.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          368.00 |          368.00 |          368.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          186.00 |          186.00 |          186.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           30.00 |           30.00 |           30.00 |            0.00 |
|    Elapsed Time |              ms |       12,444.00 |       12,444.00 |       12,444.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,629,478.52 |    8,629,478.52 |    8,629,478.52 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.57 |           29.57 |           29.57 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.95 |           14.95 |           14.95 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.41 |            2.41 |            2.41 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.74 |            4.74 |            4.74 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  107,383,976.00 |    8,629,478.52 |          115.88 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          368.00 |           29.57 |   33,814,822.28 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          186.00 |           14.95 |   66,902,444.09 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           30.00 |            2.41 |  414,795,153.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       12,444.00 |        1,000.01 |      999,988.32 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.74 |  210,912,789.83 |


