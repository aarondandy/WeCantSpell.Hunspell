# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/7/2022 4:54:20 AM_
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
|TotalBytesAllocated |           bytes |  139,431,360.00 |  139,431,360.00 |  139,431,360.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          477.00 |          477.00 |          477.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          237.00 |          237.00 |          237.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           31.00 |           31.00 |           31.00 |            0.00 |
|    Elapsed Time |              ms |       14,300.00 |       14,300.00 |       14,300.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,750,979.22 |    9,750,979.22 |    9,750,979.22 |            0.00 |
|TotalCollections [Gen0] |     collections |           33.36 |           33.36 |           33.36 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.57 |           16.57 |           16.57 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.17 |            2.17 |            2.17 |            0.00 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.05 |        1,000.05 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.13 |            4.13 |            4.13 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  139,431,360.00 |    9,750,979.22 |          102.55 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          477.00 |           33.36 |   29,977,392.45 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          237.00 |           16.57 |   60,334,245.57 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           31.00 |            2.17 |  461,265,038.71 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,300.00 |        1,000.05 |      999,945.19 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.13 |  242,359,596.61 |


