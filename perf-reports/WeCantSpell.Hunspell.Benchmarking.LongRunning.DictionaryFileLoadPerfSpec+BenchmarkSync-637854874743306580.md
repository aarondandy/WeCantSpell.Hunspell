# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/13/2022 10:57:54 PM_
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
|TotalBytesAllocated |           bytes |  184,510,504.00 |  184,510,504.00 |  184,510,504.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          329.00 |          329.00 |          329.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          167.00 |          167.00 |          167.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|    Elapsed Time |              ms |       13,375.00 |       13,375.00 |       13,375.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,796,017.08 |   13,796,017.08 |   13,796,017.08 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.60 |           24.60 |           24.60 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.49 |           12.49 |           12.49 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.94 |            1.94 |            1.94 |            0.00 |
|    Elapsed Time |              ms |        1,000.06 |        1,000.06 |        1,000.06 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.41 |            4.41 |            4.41 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  184,510,504.00 |   13,796,017.08 |           72.48 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          329.00 |           24.60 |   40,651,022.49 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          167.00 |           12.49 |   80,084,948.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |            1.94 |  514,391,784.62 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,375.00 |        1,000.06 |      999,939.17 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.41 |  226,681,125.42 |


