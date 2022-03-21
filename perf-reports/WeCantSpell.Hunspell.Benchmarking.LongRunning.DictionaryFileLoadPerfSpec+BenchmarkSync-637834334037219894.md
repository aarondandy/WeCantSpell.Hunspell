# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/21/2022 4:23:23 AM_
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
|TotalBytesAllocated |           bytes |  116,806,048.00 |  116,806,048.00 |  116,806,048.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          329.00 |          329.00 |          329.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          162.00 |          162.00 |          162.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           24.00 |           24.00 |           24.00 |            0.00 |
|    Elapsed Time |              ms |       13,432.00 |       13,432.00 |       13,432.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,696,396.06 |    8,696,396.06 |    8,696,396.06 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.49 |           24.49 |           24.49 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.06 |           12.06 |           12.06 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.79 |            1.79 |            1.79 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.03 |        1,000.03 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.39 |            4.39 |            4.39 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  116,806,048.00 |    8,696,396.06 |          114.99 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          329.00 |           24.49 |   40,825,369.30 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          162.00 |           12.06 |   82,910,780.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           24.00 |            1.79 |  559,647,770.83 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,432.00 |        1,000.03 |      999,966.24 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.39 |  227,653,330.51 |


