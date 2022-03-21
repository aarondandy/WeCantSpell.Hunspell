# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/21/2022 3:18:04 AM_
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
|TotalBytesAllocated |           bytes |  102,129,528.00 |  102,129,528.00 |  102,129,528.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          328.00 |          328.00 |          328.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          164.00 |          164.00 |          164.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|    Elapsed Time |              ms |       12,798.00 |       12,798.00 |       12,798.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,979,627.50 |    7,979,627.50 |    7,979,627.50 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.63 |           25.63 |           25.63 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.81 |           12.81 |           12.81 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.95 |            1.95 |            1.95 |            0.00 |
|    Elapsed Time |              ms |          999.94 |          999.94 |          999.94 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.61 |            4.61 |            4.61 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  102,129,528.00 |    7,979,627.50 |          125.32 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          328.00 |           25.63 |   39,020,682.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          164.00 |           12.81 |   78,041,365.24 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |            1.95 |  511,951,356.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       12,798.00 |          999.94 |    1,000,061.25 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.61 |  216,928,540.68 |


