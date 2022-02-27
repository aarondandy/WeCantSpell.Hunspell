# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/27/2022 2:32:17 AM_
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
|TotalBytesAllocated |           bytes |   29,243,144.00 |   29,243,144.00 |   29,243,144.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          741.00 |          741.00 |          741.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          253.00 |          253.00 |          253.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           34.00 |           34.00 |           34.00 |            0.00 |
|    Elapsed Time |              ms |       16,390.00 |       16,390.00 |       16,390.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,784,221.95 |    1,784,221.95 |    1,784,221.95 |            0.00 |
|TotalCollections [Gen0] |     collections |           45.21 |           45.21 |           45.21 |            0.00 |
|TotalCollections [Gen1] |     collections |           15.44 |           15.44 |           15.44 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.07 |            2.07 |            2.07 |            0.00 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.01 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.60 |            3.60 |            3.60 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   29,243,144.00 |    1,784,221.95 |          560.47 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          741.00 |           45.21 |   22,118,566.53 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          253.00 |           15.44 |   64,782,046.64 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           34.00 |            2.07 |  482,054,641.18 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,390.00 |        1,000.01 |      999,991.32 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.60 |  277,794,200.00 |


