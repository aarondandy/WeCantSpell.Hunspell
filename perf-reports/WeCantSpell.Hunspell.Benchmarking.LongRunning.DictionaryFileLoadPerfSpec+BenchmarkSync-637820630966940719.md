# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/5/2022 7:44:56 AM_
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
|TotalBytesAllocated |           bytes |  139,327,728.00 |  139,327,728.00 |  139,327,728.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          485.00 |          485.00 |          485.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          240.00 |          240.00 |          240.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           34.00 |           34.00 |           34.00 |            0.00 |
|    Elapsed Time |              ms |       14,861.00 |       14,861.00 |       14,861.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,375,438.83 |    9,375,438.83 |    9,375,438.83 |            0.00 |
|TotalCollections [Gen0] |     collections |           32.64 |           32.64 |           32.64 |            0.00 |
|TotalCollections [Gen1] |     collections |           16.15 |           16.15 |           16.15 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.29 |            2.29 |            2.29 |            0.00 |
|    Elapsed Time |              ms |        1,000.00 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.97 |            3.97 |            3.97 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  139,327,728.00 |    9,375,438.83 |          106.66 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          485.00 |           32.64 |   30,641,090.10 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          240.00 |           16.15 |   61,920,536.25 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           34.00 |            2.29 |  437,086,138.24 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,861.00 |        1,000.00 |      999,995.20 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.97 |  251,880,147.46 |


