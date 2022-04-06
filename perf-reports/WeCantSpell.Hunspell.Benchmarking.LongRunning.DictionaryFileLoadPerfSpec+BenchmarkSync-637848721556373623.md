# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/6/2022 8:02:35 PM_
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
|TotalBytesAllocated |           bytes |  106,173,688.00 |  106,173,688.00 |  106,173,688.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          331.00 |          331.00 |          331.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          166.00 |          166.00 |          166.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|    Elapsed Time |              ms |       13,390.00 |       13,390.00 |       13,390.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,929,391.69 |    7,929,391.69 |    7,929,391.69 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.72 |           24.72 |           24.72 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.40 |           12.40 |           12.40 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.02 |            2.02 |            2.02 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.41 |            4.41 |            4.41 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  106,173,688.00 |    7,929,391.69 |          126.11 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          331.00 |           24.72 |   40,452,841.99 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          166.00 |           12.40 |   80,661,992.17 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |            2.02 |  495,921,877.78 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,390.00 |        1,000.01 |      999,991.84 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.41 |  226,947,300.00 |


