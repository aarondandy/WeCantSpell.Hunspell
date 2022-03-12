# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/12/2022 2:04:47 AM_
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
|TotalBytesAllocated |           bytes |   99,484,664.00 |   99,484,664.00 |   99,484,664.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          328.00 |          328.00 |          328.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          166.00 |          166.00 |          166.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|    Elapsed Time |              ms |       11,629.00 |       11,629.00 |       11,629.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,554,752.84 |    8,554,752.84 |    8,554,752.84 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.20 |           28.20 |           28.20 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.27 |           14.27 |           14.27 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.15 |            2.15 |            2.15 |            0.00 |
|    Elapsed Time |              ms |          999.99 |          999.99 |          999.99 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.07 |            5.07 |            5.07 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   99,484,664.00 |    8,554,752.84 |          116.89 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          328.00 |           28.20 |   35,454,782.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          166.00 |           14.27 |   70,055,233.13 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |            2.15 |  465,166,748.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,629.00 |          999.99 |    1,000,014.51 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.07 |  197,104,554.24 |


