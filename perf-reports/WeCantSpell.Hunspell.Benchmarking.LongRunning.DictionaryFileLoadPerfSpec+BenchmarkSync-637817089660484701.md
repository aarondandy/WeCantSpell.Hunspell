# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/1/2022 5:22:46 AM_
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
|TotalBytesAllocated |           bytes |   37,475,608.00 |   37,475,608.00 |   37,475,608.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          641.00 |          641.00 |          641.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          256.00 |          256.00 |          256.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|    Elapsed Time |              ms |       16,369.00 |       16,369.00 |       16,369.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,289,502.73 |    2,289,502.73 |    2,289,502.73 |            0.00 |
|TotalCollections [Gen0] |     collections |           39.16 |           39.16 |           39.16 |            0.00 |
|TotalCollections [Gen1] |     collections |           15.64 |           15.64 |           15.64 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.20 |            2.20 |            2.20 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |        1,000.03 |        1,000.03 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.60 |            3.60 |            3.60 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   37,475,608.00 |    2,289,502.73 |          436.78 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          641.00 |           39.16 |   25,535,801.40 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          256.00 |           15.64 |   63,939,252.73 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |            2.20 |  454,679,130.56 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,369.00 |        1,000.03 |      999,966.32 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.60 |  277,431,333.90 |


