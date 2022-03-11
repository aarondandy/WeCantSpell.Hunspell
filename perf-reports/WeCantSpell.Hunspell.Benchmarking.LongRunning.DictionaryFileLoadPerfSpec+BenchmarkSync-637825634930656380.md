# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/11/2022 2:44:53 AM_
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
|TotalBytesAllocated |           bytes |   99,511,592.00 |   99,511,592.00 |   99,511,592.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          331.00 |          331.00 |          331.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          168.00 |          168.00 |          168.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|    Elapsed Time |              ms |       11,321.00 |       11,321.00 |       11,321.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,790,067.42 |    8,790,067.42 |    8,790,067.42 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.24 |           29.24 |           29.24 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.84 |           14.84 |           14.84 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.38 |            2.38 |            2.38 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.21 |            5.21 |            5.21 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   99,511,592.00 |    8,790,067.42 |          113.76 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          331.00 |           29.24 |   34,202,155.29 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          168.00 |           14.84 |   67,386,389.29 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |            2.38 |  419,293,088.89 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,321.00 |        1,000.01 |      999,992.35 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.21 |  191,879,888.14 |


