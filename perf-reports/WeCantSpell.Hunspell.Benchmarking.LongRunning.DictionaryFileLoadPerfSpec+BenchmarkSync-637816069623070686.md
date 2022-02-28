# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/28/2022 1:02:42 AM_
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
|TotalBytesAllocated |           bytes |   37,720,360.00 |   37,720,360.00 |   37,720,360.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          640.00 |          640.00 |          640.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          259.00 |          259.00 |          259.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           35.00 |           35.00 |           35.00 |            0.00 |
|    Elapsed Time |              ms |       16,132.00 |       16,132.00 |       16,132.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,338,202.82 |    2,338,202.82 |    2,338,202.82 |            0.00 |
|TotalCollections [Gen0] |     collections |           39.67 |           39.67 |           39.67 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.05 |           16.05 |           16.05 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.17 |            2.17 |            2.17 |            0.00 |
|    Elapsed Time |              ms |          999.99 |          999.99 |          999.99 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.66 |            3.66 |            3.66 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   37,720,360.00 |    2,338,202.82 |          427.68 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          640.00 |           39.67 |   25,206,565.47 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          259.00 |           16.05 |   62,286,493.82 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           35.00 |            2.17 |  460,920,054.29 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,132.00 |          999.99 |    1,000,012.52 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.66 |  273,427,150.85 |


