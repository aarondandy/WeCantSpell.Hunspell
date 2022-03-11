# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/11/2022 1:46:46 AM_
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
|TotalBytesAllocated |           bytes |   99,476,520.00 |   99,476,520.00 |   99,476,520.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          331.00 |          331.00 |          331.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          167.00 |          167.00 |          167.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           28.00 |           28.00 |           28.00 |            0.00 |
|    Elapsed Time |              ms |       11,469.00 |       11,469.00 |       11,469.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,673,767.21 |    8,673,767.21 |    8,673,767.21 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.86 |           28.86 |           28.86 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.56 |           14.56 |           14.56 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.44 |            2.44 |            2.44 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.03 |        1,000.03 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.14 |            5.14 |            5.14 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   99,476,520.00 |    8,673,767.21 |          115.29 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          331.00 |           28.86 |   34,648,531.12 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          167.00 |           14.56 |   68,674,633.53 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           28.00 |            2.44 |  409,595,135.71 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,469.00 |        1,000.03 |      999,970.69 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.14 |  194,384,132.20 |


