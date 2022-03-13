# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/13/2022 10:15:15 PM_
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
|TotalBytesAllocated |           bytes |  105,291,784.00 |  105,291,784.00 |  105,291,784.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          326.00 |          326.00 |          326.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          163.00 |          163.00 |          163.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           24.00 |           24.00 |           24.00 |            0.00 |
|    Elapsed Time |              ms |       11,561.00 |       11,561.00 |       11,561.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,106,950.57 |    9,106,950.57 |    9,106,950.57 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.20 |           28.20 |           28.20 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.10 |           14.10 |           14.10 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.08 |            2.08 |            2.08 |            0.00 |
|    Elapsed Time |              ms |          999.94 |          999.94 |          999.94 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.10 |            5.10 |            5.10 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  105,291,784.00 |    9,106,950.57 |          109.81 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          326.00 |           28.20 |   35,465,321.78 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          163.00 |           14.10 |   70,930,643.56 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           24.00 |            2.08 |  481,737,287.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,561.00 |          999.94 |    1,000,060.11 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.10 |  195,960,930.51 |


