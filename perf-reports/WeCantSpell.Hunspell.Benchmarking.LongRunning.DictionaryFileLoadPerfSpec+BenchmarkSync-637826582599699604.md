# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/12/2022 5:04:19 AM_
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
|TotalBytesAllocated |           bytes |  103,628,400.00 |  103,628,400.00 |  103,628,400.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          328.00 |          328.00 |          328.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          165.00 |          165.00 |          165.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|    Elapsed Time |              ms |       11,612.00 |       11,612.00 |       11,612.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,924,454.75 |    8,924,454.75 |    8,924,454.75 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.25 |           28.25 |           28.25 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.21 |           14.21 |           14.21 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.15 |            2.15 |            2.15 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.02 |        1,000.02 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.08 |            5.08 |            5.08 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,628,400.00 |    8,924,454.75 |          112.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          328.00 |           28.25 |   35,401,629.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          165.00 |           14.21 |   70,374,149.09 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |            2.15 |  464,469,384.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,612.00 |        1,000.02 |      999,977.14 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.08 |  196,809,061.02 |


