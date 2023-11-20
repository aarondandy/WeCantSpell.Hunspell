# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_11/20/2023 3:55:43 AM_
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
|TotalBytesAllocated |           bytes |   73,709,296.00 |   73,709,296.00 |   73,709,296.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          248.00 |          248.00 |          248.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          135.00 |          135.00 |          135.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           22.00 |           22.00 |           22.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,296,548.73 |    5,296,548.73 |    5,296,548.73 |            0.00 |
|TotalCollections [Gen0] |     collections |           17.82 |           17.82 |           17.82 |            0.00 |
|TotalCollections [Gen1] |     collections |            9.70 |            9.70 |            9.70 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.58 |            1.58 |            1.58 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.24 |            4.24 |            4.24 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   73,709,296.00 |    5,296,548.73 |          188.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          248.00 |           17.82 |   56,114,824.60 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          135.00 |            9.70 |  103,085,011.11 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           22.00 |            1.58 |  632,567,113.64 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.24 |  235,872,483.05 |


