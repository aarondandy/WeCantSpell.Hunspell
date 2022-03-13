# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/13/2022 12:15:40 AM_
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
|TotalBytesAllocated |           bytes |  103,635,200.00 |  103,635,200.00 |  103,635,200.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          327.00 |          327.00 |          327.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          164.00 |          164.00 |          164.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           24.00 |           24.00 |           24.00 |            0.00 |
|    Elapsed Time |              ms |       11,573.00 |       11,573.00 |       11,573.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,954,607.52 |    8,954,607.52 |    8,954,607.52 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.25 |           28.25 |           28.25 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.17 |           14.17 |           14.17 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.07 |            2.07 |            2.07 |            0.00 |
|    Elapsed Time |              ms |          999.97 |          999.97 |          999.97 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.10 |            5.10 |            5.10 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,635,200.00 |    8,954,607.52 |          111.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          327.00 |           28.25 |   35,392,641.90 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          164.00 |           14.17 |   70,569,475.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           24.00 |            2.07 |  482,224,745.83 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,573.00 |          999.97 |    1,000,034.04 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.10 |  196,159,218.64 |


