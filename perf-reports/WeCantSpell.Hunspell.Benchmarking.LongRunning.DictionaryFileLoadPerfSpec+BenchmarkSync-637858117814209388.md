# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/17/2022 5:03:01 PM_
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
|TotalBytesAllocated |           bytes |  106,175,960.00 |  106,175,960.00 |  106,175,960.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          332.00 |          332.00 |          332.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          167.00 |          167.00 |          167.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           28.00 |           28.00 |           28.00 |            0.00 |
|    Elapsed Time |              ms |       13,289.00 |       13,289.00 |       13,289.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,989,815.21 |    7,989,815.21 |    7,989,815.21 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.98 |           24.98 |           24.98 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.57 |           12.57 |           12.57 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.11 |            2.11 |            2.11 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.44 |            4.44 |            4.44 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  106,175,960.00 |    7,989,815.21 |          125.16 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          332.00 |           24.98 |   40,026,846.69 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          167.00 |           12.57 |   79,574,329.94 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           28.00 |            2.11 |  474,604,039.29 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,289.00 |        1,000.01 |      999,993.46 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.44 |  225,235,815.25 |


