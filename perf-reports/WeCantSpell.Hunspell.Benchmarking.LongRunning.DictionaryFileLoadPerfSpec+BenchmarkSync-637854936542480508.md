# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/14/2022 12:40:54 AM_
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
|TotalBytesAllocated |           bytes |  184,511,904.00 |  184,511,904.00 |  184,511,904.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          329.00 |          329.00 |          329.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          167.00 |          167.00 |          167.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|    Elapsed Time |              ms |       13,567.00 |       13,567.00 |       13,567.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,600,685.66 |   13,600,685.66 |   13,600,685.66 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.25 |           24.25 |           24.25 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.31 |           12.31 |           12.31 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.92 |            1.92 |            1.92 |            0.00 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.05 |        1,000.05 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.35 |            4.35 |            4.35 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  184,511,904.00 |   13,600,685.66 |           73.53 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          329.00 |           24.25 |   41,235,160.49 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          167.00 |           12.31 |   81,235,735.33 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |            1.92 |  521,783,376.92 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,567.00 |        1,000.05 |      999,953.40 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.35 |  229,938,437.29 |


