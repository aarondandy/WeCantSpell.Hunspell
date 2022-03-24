# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/24/2022 4:39:13 AM_
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
|TotalBytesAllocated |           bytes |  111,221,360.00 |  111,221,360.00 |  111,221,360.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          337.00 |          337.00 |          337.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          171.00 |          171.00 |          171.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           32.00 |           32.00 |           32.00 |            0.00 |
|    Elapsed Time |              ms |       13,892.00 |       13,892.00 |       13,892.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,006,022.37 |    8,006,022.37 |    8,006,022.37 |            0.00 |
|TotalCollections [Gen0] |     collections |           24.26 |           24.26 |           24.26 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.31 |           12.31 |           12.31 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.30 |            2.30 |            2.30 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.98 |          999.98 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.25 |            4.25 |            4.25 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  111,221,360.00 |    8,006,022.37 |          124.91 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          337.00 |           24.26 |   41,223,181.01 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          171.00 |           12.31 |   81,241,005.85 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           32.00 |            2.30 |  434,131,625.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       13,892.00 |          999.98 |    1,000,015.26 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.25 |  235,461,220.34 |


