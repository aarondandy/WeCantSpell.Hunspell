# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/3/2022 4:08:45 AM_
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
|TotalBytesAllocated |           bytes |   38,464,856.00 |   38,464,856.00 |   38,464,856.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          575.00 |          575.00 |          575.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          250.00 |          250.00 |          250.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           33.00 |           33.00 |           33.00 |            0.00 |
|    Elapsed Time |              ms |       15,128.00 |       15,128.00 |       15,128.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,542,529.24 |    2,542,529.24 |    2,542,529.24 |            0.00 |
|TotalCollections [Gen0] |     collections |           38.01 |           38.01 |           38.01 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.53 |           16.53 |           16.53 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.18 |            2.18 |            2.18 |            0.00 |
|    Elapsed Time |              ms |          999.96 |          999.96 |          999.96 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.90 |            3.90 |            3.90 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   38,464,856.00 |    2,542,529.24 |          393.31 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          575.00 |           38.01 |   26,310,573.22 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          250.00 |           16.53 |   60,514,318.40 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           33.00 |            2.18 |  458,441,806.06 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,128.00 |          999.96 |    1,000,038.31 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.90 |  256,416,603.39 |


