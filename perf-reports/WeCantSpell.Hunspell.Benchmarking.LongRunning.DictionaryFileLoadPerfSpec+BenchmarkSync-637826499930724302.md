# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/12/2022 2:46:33 AM_
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
|TotalBytesAllocated |           bytes |  103,617,232.00 |  103,617,232.00 |  103,617,232.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          328.00 |          328.00 |          328.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          165.00 |          165.00 |          165.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|    Elapsed Time |              ms |       11,580.00 |       11,580.00 |       11,580.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,947,588.05 |    8,947,588.05 |    8,947,588.05 |            0.00 |
|TotalCollections [Gen0] |     collections |           28.32 |           28.32 |           28.32 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.25 |           14.25 |           14.25 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.16 |            2.16 |            2.16 |            0.00 |
|    Elapsed Time |              ms |          999.96 |          999.96 |          999.96 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            5.09 |            5.09 |            5.09 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  103,617,232.00 |    8,947,588.05 |          111.76 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          328.00 |           28.32 |   35,306,296.34 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          165.00 |           14.25 |   70,184,637.58 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |            2.16 |  463,218,608.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       11,580.00 |          999.96 |    1,000,040.17 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            5.09 |  196,279,071.19 |


