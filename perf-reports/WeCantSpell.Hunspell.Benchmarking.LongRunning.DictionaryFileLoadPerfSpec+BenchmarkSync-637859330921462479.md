# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_4/19/2022 2:44:52 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   74,315,024.00 |   74,315,024.00 |   74,315,024.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          263.00 |          263.00 |          263.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          135.00 |          135.00 |          135.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           23.00 |           23.00 |           23.00 |            0.00 |
|    Elapsed Time |              ms |       12,657.00 |       12,657.00 |       12,657.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,871,266.84 |    5,871,266.84 |    5,871,266.84 |            0.00 |
|TotalCollections [Gen0] |     collections |           20.78 |           20.78 |           20.78 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.67 |           10.67 |           10.67 |            0.00 |
|TotalCollections [Gen2] |     collections |            1.82 |            1.82 |            1.82 |            0.00 |
|    Elapsed Time |              ms |          999.97 |          999.97 |          999.97 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            4.66 |            4.66 |            4.66 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   74,315,024.00 |    5,871,266.84 |          170.32 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          263.00 |           20.78 |   48,127,029.28 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          135.00 |           10.67 |   93,758,582.96 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           23.00 |            1.82 |  550,322,117.39 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       12,657.00 |          999.97 |    1,000,032.29 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            4.66 |  214,532,350.85 |


