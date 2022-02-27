# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/27/2022 4:20:27 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   28,049,064.00 |   28,049,064.00 |   28,049,064.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          648.00 |          648.00 |          648.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          225.00 |          225.00 |          225.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           33.00 |           33.00 |           33.00 |            0.00 |
|    Elapsed Time |              ms |       15,929.00 |       15,929.00 |       15,929.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,760,885.56 |    1,760,885.56 |    1,760,885.56 |            0.00 |
|TotalCollections [Gen0] |     collections |           40.68 |           40.68 |           40.68 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.13 |           14.13 |           14.13 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.07 |            2.07 |            2.07 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.70 |            3.70 |            3.70 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   28,049,064.00 |    1,760,885.56 |          567.90 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          648.00 |           40.68 |   24,581,718.21 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          225.00 |           14.13 |   70,795,348.44 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           33.00 |            2.07 |  482,695,557.58 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,929.00 |        1,000.00 |      999,997.07 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.70 |  269,982,261.02 |


