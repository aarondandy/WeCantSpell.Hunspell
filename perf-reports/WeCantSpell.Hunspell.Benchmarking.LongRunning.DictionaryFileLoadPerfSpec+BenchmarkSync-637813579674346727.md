# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_2/25/2022 3:52:47 AM_
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
|TotalBytesAllocated |           bytes |  116,267,312.00 |  116,267,312.00 |  116,267,312.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          696.00 |          696.00 |          696.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          243.00 |          243.00 |          243.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           40.00 |           40.00 |           40.00 |            0.00 |
|    Elapsed Time |              ms |       14,859.00 |       14,859.00 |       14,859.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,824,989.22 |    7,824,989.22 |    7,824,989.22 |            0.00 |
|TotalCollections [Gen0] |     collections |           46.84 |           46.84 |           46.84 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.35 |           16.35 |           16.35 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.69 |            2.69 |            2.69 |            0.00 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.04 |        1,000.04 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.97 |            3.97 |            3.97 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  116,267,312.00 |    7,824,989.22 |          127.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          696.00 |           46.84 |   21,348,366.24 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          243.00 |           16.35 |   61,145,937.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           40.00 |            2.69 |  371,461,572.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,859.00 |        1,000.04 |      999,963.85 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.97 |  251,838,354.24 |


