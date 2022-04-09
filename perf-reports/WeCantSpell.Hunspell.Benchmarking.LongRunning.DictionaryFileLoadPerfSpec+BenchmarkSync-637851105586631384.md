# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/9/2022 2:15:58 PM_
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
|TotalBytesAllocated |           bytes |  103,602,288.00 |  103,602,288.00 |  103,602,288.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          335.00 |          335.00 |          335.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          171.00 |          171.00 |          171.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           30.00 |           30.00 |           30.00 |            0.00 |
|    Elapsed Time |              ms |       11,489.00 |       11,489.00 |       11,489.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,016,950.51 |    9,016,950.51 |    9,016,950.51 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.16 |           29.16 |           29.16 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.88 |           14.88 |           14.88 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.61 |            2.61 |            2.61 |            0.00 |
|    Elapsed Time |              ms |          999.94 |          999.94 |          999.94 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.14 |            5.14 |            5.14 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,602,288.00 |    9,016,950.51 |          110.90 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          335.00 |           29.16 |   34,297,688.66 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          171.00 |           14.88 |   67,191,378.36 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           30.00 |            2.61 |  382,990,856.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,489.00 |          999.94 |    1,000,063.16 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.14 |  194,741,113.56 |


