# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_5/8/2022 8:56:59 PM_
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
|TotalBytesAllocated |           bytes |   86,260,424.00 |   86,260,424.00 |   86,260,424.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          245.00 |          245.00 |          245.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          131.00 |          131.00 |          131.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           20.00 |           20.00 |           20.00 |            0.00 |
|    Elapsed Time |              ms |       12,481.00 |       12,481.00 |       12,481.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,911,495.15 |    6,911,495.15 |    6,911,495.15 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.63 |           19.63 |           19.63 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.50 |           10.50 |           10.50 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.60 |            1.60 |            1.60 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.02 |        1,000.02 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.73 |            4.73 |            4.73 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   86,260,424.00 |    6,911,495.15 |          144.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          245.00 |           19.63 |   50,941,707.35 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          131.00 |           10.50 |   95,272,658.78 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           20.00 |            1.60 |  624,035,915.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       12,481.00 |        1,000.02 |      999,977.43 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.73 |  211,537,598.31 |


