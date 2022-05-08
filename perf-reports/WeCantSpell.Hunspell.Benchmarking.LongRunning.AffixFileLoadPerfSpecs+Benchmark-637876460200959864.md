# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_5/8/2022 10:33:40 PM_
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
|TotalBytesAllocated |           bytes |    9,984,696.00 |    9,984,696.00 |    9,984,696.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           22.00 |           22.00 |           22.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.00 |            4.00 |            4.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,676,798.74 |    8,676,798.74 |    8,676,798.74 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.12 |           19.12 |           19.12 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.43 |           10.43 |           10.43 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.48 |            3.48 |            3.48 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           51.27 |           51.27 |           51.27 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    9,984,696.00 |    8,676,798.74 |          115.25 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           22.00 |           19.12 |   52,306,136.36 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           10.43 |   95,894,583.33 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            4.00 |            3.48 |  287,683,750.00 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           51.27 |   19,503,983.05 |


