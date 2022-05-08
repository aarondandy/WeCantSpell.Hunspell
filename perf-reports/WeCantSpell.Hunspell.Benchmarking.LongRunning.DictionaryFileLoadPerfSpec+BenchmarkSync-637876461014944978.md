# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_5/8/2022 10:35:01 PM_
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
|TotalBytesAllocated |           bytes |   86,265,656.00 |   86,265,656.00 |   86,265,656.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          244.00 |          244.00 |          244.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          130.00 |          130.00 |          130.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           19.00 |           19.00 |           19.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,959,093.23 |    6,959,093.23 |    6,959,093.23 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.68 |           19.68 |           19.68 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.49 |           10.49 |           10.49 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.53 |            1.53 |            1.53 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.76 |            4.76 |            4.76 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   86,265,656.00 |    6,959,093.23 |          143.70 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          244.00 |           19.68 |   50,803,712.30 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          130.00 |           10.49 |   95,354,660.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           19.00 |            1.53 |  652,426,621.05 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.76 |  210,103,488.14 |


