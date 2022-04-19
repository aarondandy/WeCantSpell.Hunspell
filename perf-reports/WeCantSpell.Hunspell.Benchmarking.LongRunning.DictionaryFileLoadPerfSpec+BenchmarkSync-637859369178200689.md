# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/19/2022 3:48:37 AM_
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
|TotalBytesAllocated |           bytes |  133,379,344.00 |  133,379,344.00 |  133,379,344.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          245.00 |          245.00 |          245.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          131.00 |          131.00 |          131.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           20.00 |           20.00 |           20.00 |            0.00 |
|    Elapsed Time |              ms |       12,199.00 |       12,199.00 |       12,199.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   10,933,367.52 |   10,933,367.52 |   10,933,367.52 |            0.00 |
|TotalCollections [Gen0] |     collections |           20.08 |           20.08 |           20.08 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.74 |           10.74 |           10.74 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.64 |            1.64 |            1.64 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.98 |          999.98 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.84 |            4.84 |            4.84 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  133,379,344.00 |   10,933,367.52 |           91.46 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          245.00 |           20.08 |   49,793,028.98 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          131.00 |           10.74 |   93,124,367.18 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           20.00 |            1.64 |  609,964,605.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       12,199.00 |          999.98 |    1,000,023.94 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.84 |  206,767,662.71 |


