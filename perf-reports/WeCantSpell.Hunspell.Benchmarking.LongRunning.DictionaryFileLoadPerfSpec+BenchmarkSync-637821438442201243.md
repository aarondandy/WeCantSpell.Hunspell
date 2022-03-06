# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/6/2022 6:10:44 AM_
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
|TotalBytesAllocated |           bytes |  139,992,648.00 |  139,992,648.00 |  139,992,648.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          488.00 |          488.00 |          488.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          243.00 |          243.00 |          243.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|    Elapsed Time |              ms |       14,364.00 |       14,364.00 |       14,364.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,745,833.62 |    9,745,833.62 |    9,745,833.62 |            0.00 |
|TotalCollections [Gen0] |     collections |           33.97 |           33.97 |           33.97 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.92 |           16.92 |           16.92 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.51 |            2.51 |            2.51 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.98 |          999.98 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.11 |            4.11 |            4.11 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  139,992,648.00 |    9,745,833.62 |          102.61 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          488.00 |           33.97 |   29,435,160.86 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          243.00 |           16.92 |   59,112,586.42 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |            2.51 |  399,009,958.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,364.00 |          999.98 |    1,000,024.96 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.11 |  243,463,703.39 |


