# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/11/2022 3:56:26 AM_
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
|TotalBytesAllocated |           bytes |  103,608,520.00 |  103,608,520.00 |  103,608,520.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          336.00 |          336.00 |          336.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          172.00 |          172.00 |          172.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           31.00 |           31.00 |           31.00 |            0.00 |
|    Elapsed Time |              ms |       11,752.00 |       11,752.00 |       11,752.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,816,766.11 |    8,816,766.11 |    8,816,766.11 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.59 |           28.59 |           28.59 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.64 |           14.64 |           14.64 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.64 |            2.64 |            2.64 |            0.00 |
|    Elapsed Time |              ms |        1,000.06 |        1,000.06 |        1,000.06 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.02 |            5.02 |            5.02 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,608,520.00 |    8,816,766.11 |          113.42 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          336.00 |           28.59 |   34,974,126.19 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          172.00 |           14.64 |   68,321,548.84 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           31.00 |            2.64 |  379,074,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,752.00 |        1,000.06 |      999,940.98 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.02 |  199,174,684.75 |


