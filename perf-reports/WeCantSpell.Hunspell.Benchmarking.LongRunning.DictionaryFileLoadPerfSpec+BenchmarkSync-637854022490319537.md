# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/12/2022 11:17:29 PM_
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
|TotalBytesAllocated |           bytes |  184,511,048.00 |  184,511,048.00 |  184,511,048.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          329.00 |          329.00 |          329.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          167.00 |          167.00 |          167.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|    Elapsed Time |              ms |       13,439.00 |       13,439.00 |       13,439.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,729,919.76 |   13,729,919.76 |   13,729,919.76 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.48 |           24.48 |           24.48 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.43 |           12.43 |           12.43 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.93 |            1.93 |            1.93 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.03 |        1,000.03 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.39 |            4.39 |            4.39 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  184,511,048.00 |   13,729,919.76 |           72.83 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          329.00 |           24.48 |   40,846,841.34 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          167.00 |           12.43 |   80,470,723.35 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |            1.93 |  516,869,646.15 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,439.00 |        1,000.03 |      999,971.04 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.39 |  227,773,064.41 |


