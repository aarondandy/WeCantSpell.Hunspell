# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/6/2022 1:14:45 AM_
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
|TotalBytesAllocated |           bytes |   38,051,848.00 |   38,051,848.00 |   38,051,848.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          488.00 |          488.00 |          488.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          244.00 |          244.00 |          244.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|    Elapsed Time |              ms |       14,510.00 |       14,510.00 |       14,510.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,622,404.95 |    2,622,404.95 |    2,622,404.95 |            0.00 |
|TotalCollections [Gen0] |     collections |           33.63 |           33.63 |           33.63 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.82 |           16.82 |           16.82 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.48 |            2.48 |            2.48 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.98 |          999.98 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.07 |            4.07 |            4.07 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   38,051,848.00 |    2,622,404.95 |          381.33 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          488.00 |           33.63 |   29,734,194.26 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          244.00 |           16.82 |   59,468,388.52 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |            2.48 |  403,063,522.22 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,510.00 |          999.98 |    1,000,019.77 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.07 |  245,937,064.41 |


