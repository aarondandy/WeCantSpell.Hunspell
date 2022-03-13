# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/13/2022 4:20:02 AM_
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
|TotalBytesAllocated |           bytes |  103,632,544.00 |  103,632,544.00 |  103,632,544.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          328.00 |          328.00 |          328.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          165.00 |          165.00 |          165.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|    Elapsed Time |              ms |       11,413.00 |       11,413.00 |       11,413.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,080,109.21 |    9,080,109.21 |    9,080,109.21 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.74 |           28.74 |           28.74 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.46 |           14.46 |           14.46 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.19 |            2.19 |            2.19 |            0.00 |
|    Elapsed Time |              ms |          999.99 |          999.99 |          999.99 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.17 |            5.17 |            5.17 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,632,544.00 |    9,080,109.21 |          110.13 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          328.00 |           28.74 |   34,796,153.96 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          165.00 |           14.46 |   69,170,536.36 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |            2.19 |  456,525,540.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,413.00 |          999.99 |    1,000,012.14 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.17 |  193,443,025.42 |


