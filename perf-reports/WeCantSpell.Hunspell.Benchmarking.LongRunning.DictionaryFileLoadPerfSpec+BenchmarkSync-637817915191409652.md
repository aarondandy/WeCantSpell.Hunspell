# WeCantSpell.Hunspell.Benchmarking.LongRunning.DictionaryFileLoadPerfSpec+BenchmarkSync
__Ensure that dictionary files can be loaded quickly.__
_3/2/2022 4:18:39 AM_
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
|TotalBytesAllocated |           bytes |  139,395,296.00 |  139,395,296.00 |  139,395,296.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          578.00 |          578.00 |          578.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          249.00 |          249.00 |          249.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           33.00 |           33.00 |           33.00 |            0.00 |
|    Elapsed Time |              ms |       15,613.00 |       15,613.00 |       15,613.00 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,927,925.09 |    8,927,925.09 |    8,927,925.09 |            0.00 |
|TotalCollections [Gen0] |     collections |           37.02 |           37.02 |           37.02 |            0.00 |
|TotalCollections [Gen1] |     collections |           15.95 |           15.95 |           15.95 |            0.00 |
|TotalCollections [Gen2] |     collections |            2.11 |            2.11 |            2.11 |            0.00 |
|    Elapsed Time |              ms |          999.97 |          999.97 |          999.97 |            0.00 |
|[Counter] DictionaryFilesLoaded |      operations |            3.78 |            3.78 |            3.78 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  139,395,296.00 |    8,927,925.09 |          112.01 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          578.00 |           37.02 |   27,012,808.65 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          249.00 |           15.95 |   62,704,431.33 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           33.00 |            2.11 |  473,133,436.36 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,613.00 |          999.97 |    1,000,025.84 |

#### [Counter] DictionaryFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.78 |  264,633,955.93 |


