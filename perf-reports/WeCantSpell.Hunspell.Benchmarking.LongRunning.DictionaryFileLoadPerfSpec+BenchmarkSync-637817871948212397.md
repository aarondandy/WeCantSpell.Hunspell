# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/2/2022 3:06:34 AM_
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
|TotalBytesAllocated |           bytes |   37,495,272.00 |   37,495,272.00 |   37,495,272.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          641.00 |          641.00 |          641.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          256.00 |          256.00 |          256.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|    Elapsed Time |              ms |       16,677.00 |       16,677.00 |       16,677.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,248,317.99 |    2,248,317.99 |    2,248,317.99 |            0.00 |
|TotalCollections [Gen0] |     collections |           38.44 |           38.44 |           38.44 |            0.00 |
|TotalCollections [Gen1] |     collections |           15.35 |           15.35 |           15.35 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.16 |            2.16 |            2.16 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.54 |            3.54 |            3.54 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   37,495,272.00 |    2,248,317.99 |          444.78 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          641.00 |           38.44 |   26,017,211.23 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          256.00 |           15.35 |   65,144,657.81 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |            2.16 |  463,250,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       16,677.00 |        1,000.00 |    1,000,001.94 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.54 |  282,661,566.10 |


