# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_11/20/2023 12:52:21 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.22631.0
ProcessorCount=16
CLR=6.0.25,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   73,706,960.00 |   73,706,960.00 |   73,706,960.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          245.00 |          245.00 |          245.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          133.00 |          133.00 |          133.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           20.00 |           20.00 |           20.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,856,886.00 |    5,856,886.00 |    5,856,886.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.47 |           19.47 |           19.47 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.57 |           10.57 |           10.57 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.59 |            1.59 |            1.59 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.69 |            4.69 |            4.69 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   73,706,960.00 |    5,856,886.00 |          170.74 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          245.00 |           19.47 |   51,365,987.76 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          133.00 |           10.57 |   94,621,556.39 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           20.00 |            1.59 |  629,233,350.00 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.69 |  213,299,440.68 |


